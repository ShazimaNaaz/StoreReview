using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using JewelleryStore.Common;
using JewelleryStore.Logging;
using JewelleryStore.Service.EstimationService;
using PropertyChanged;
using Xamarin.Forms;

namespace JewelleryStore.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class EstimationPageModel : FreshBasePageModel
    {
        #region properties, fields
        IloggerService _logger;
        IPrintService _printService;

        public string GoldPrice { get; set; }
        public string Weight { get; set; }
        public string TotalPrice { get; set; }
        public string Discount { get; set; } = "0";
        public string SubTitle { get; set; }
        public bool isDiscountVisible { get; set; }

        public string GoldPriceConstant = Helper.Resources.StringResources.GoldPriceConstant;
        public string TotalPriceConstant = Helper.Resources.StringResources.TotalPriceConstant;
        public string WeightConstant = Helper.Resources.StringResources.WeightConstant;
        public string DiscountConstant = Helper.Resources.StringResources.DiscountConstant;
        public string WelcomeConstant = Helper.Resources.StringResources.WelcomeConstant;
        public string UserConstant = Helper.Resources.StringResources.UserConstant;
        public string PrevilegedConstant = Helper.Resources.StringResources.PrevilegedConstant;
        public string ValidInputsConstant = Helper.Resources.StringResources.ValidInputsConstant;
        public string PriceWeightConstant = Helper.Resources.StringResources.PriceWeightConstant;
        public string CloseConstant = Helper.Resources.StringResources.CloseConstant;
        public string SavedToFileConstant = Helper.Resources.StringResources.SavedToFileConstant;
        public string OkayConstant = Helper.Resources.StringResources.OkayConstant;
        public string BillingConstant = Helper.Resources.StringResources.BillingConstant;

        #endregion

        /// <summary>
        /// Dependency injection and print functionality is implemented follows Strategy design principle
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public EstimationPageModel(IloggerService loggerService, IPrintService printService)
        {
            _logger = loggerService;
            _printService = printService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            try
            {
                if (null != initData)
                { 
                    SubTitle = WelcomeConstant + initData.ToString() + UserConstant;
                    isDiscountVisible = initData.Equals(PrevilegedConstant) ? true : false;
                    Discount = initData.Equals(PrevilegedConstant) ? "2" : "0";
                }
            }
            catch (Exception ex)
            {
                _logger.LogRecord("Error in EstimationPageModel  : Init", LogType.Error, ex);
            }
        }

        #region Commands
        public ICommand CalculateCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    try
                    {
                        CalculateTotalAmount();
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogRecord("Error in EstimationPageModel  : CalculateCommand : Param = " + param, LogType.Error, ex);
                    }
                });
            }
        }

        public ICommand PrintToScreenCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    try
                    {
                        var totalAmount = CalculateTotalAmount();
                        if (totalAmount > 1)
                        {
                            var list = new List<string>();
                            list.Add(totalAmount.ToString());
                            var response = await _printService.PrintBill(BillMode.PrintToScreen, list);
                            await ShowPopup(response);
                        }
                        else
                        {
                            await CoreMethods.DisplayAlert(ValidInputsConstant, PriceWeightConstant, OkayConstant);
                        }
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogRecord("Error in EstimationPageModel  : PrintToScreenCommand : Param = " + param, LogType.Error, ex);
                    }
                });
            }
        }

        public ICommand PrintToFileCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    try
                    {
                        CalculateTotalAmount();
                        SaveInfoToFile();
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogRecord("Error in EstimationPageModel  : PrintToFileCommand : Param = " + param, LogType.Error, ex);
                    }
                });
            }
        }

        public ICommand PrintToPaperCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    try
                    {
                        await _printService.PrintBill(BillMode.PrintToPaper);
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogRecord("Error in EstimationPageModel  : PrintToPaperCommand : Param = " + param, LogType.Error, ex);
                    }
                });
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    try
                    {
                        ClearAllFields();
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogRecord("Error in EstimationPageModel  : CloseCommand : Param = " + param, LogType.Error, ex);
                    }
                });
            }
        }

        #endregion


        #region private methods
        /// <summary>
        /// Clears all entry values
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private void ClearAllFields()
        {
            GoldPrice = string.Empty;
            TotalPrice = string.Empty;
            Weight = string.Empty;
        }


        /// <summary>
        /// Saves the bill to a text file
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async void SaveInfoToFile()
        {
            if (!String.IsNullOrEmpty(TotalPrice) &&  Int16.Parse(TotalPrice) > 1)
            {
                List<string> list = new List<string>();
                list.Add(GoldPriceConstant + " : " + GoldPrice);
                list.Add(WeightConstant + " : " + Weight);
                list.Add(DiscountConstant + " : " + Discount);
                list.Add(TotalPriceConstant + " : " + TotalPrice);
                var response = await _printService.PrintBill(BillMode.PrintToFile, list);
                await CoreMethods.DisplayAlert(response, SavedToFileConstant, OkayConstant);
            }
            else
            {
                await CoreMethods.DisplayAlert(ValidInputsConstant, PriceWeightConstant, OkayConstant);
            }
        }

        /// <summary>
        /// Displays the total amount in the Message box
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task ShowPopup(string message)
        {
            await CoreMethods.DisplayAlert(BillingConstant, message.ToString() , OkayConstant);
        }

        /// <summary>
        /// Calculates the total amount , with certain discount only for priveleged user
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private int CalculateTotalAmount()
        {
            int totalValue = 0;
            if(!String.IsNullOrEmpty(GoldPrice) && !String.IsNullOrEmpty(Weight))
            {
                totalValue = Int16.Parse(GoldPrice) * Int16.Parse(Weight) - Int16.Parse(Discount);
                TotalPrice = totalValue.ToString();
            }
            return totalValue;
        }

        #endregion
    }
}
