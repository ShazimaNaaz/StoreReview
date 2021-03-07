using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using JewelleryStore.Common;
using JewelleryStore.Logging;
using PropertyChanged;
using Xamarin.Forms;

namespace JewelleryStore.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class EstimationPageModel : FreshBasePageModel
    {
        IloggerService _logger;

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

        public EstimationPageModel(IloggerService loggerService)
        {
            _logger = loggerService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            try
            {
                if (null != initData)
                { 
                    SubTitle = "Welcome :" + initData.ToString() + "User";
                    isDiscountVisible = initData.Equals("Previleged") ? true : false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogRecord("Error in EstimationPageModel  : Init", LogType.Error, ex);
            }
        }

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
                        await ShowPopup();
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
                        throw new NotImplementedException();
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

        private void ClearAllFields()
        {
            GoldPrice = string.Empty;
            TotalPrice = string.Empty;
            Weight = string.Empty;
        }

        private async void SaveInfoToFile()
        {
            if (!String.IsNullOrEmpty(TotalPrice) &&  Int16.Parse(TotalPrice) > 1)
            {
                List<string> list = new List<string>();
                list.Add(GoldPriceConstant + " : " + GoldPrice);
                list.Add(WeightConstant + " : " + Weight);
                list.Add(DiscountConstant + " : " + Discount);
                list.Add(TotalPriceConstant + " : " + TotalPrice);
                var response = Utilities.SaveBillToFile(list);

                await CoreMethods.DisplayAlert(response, "Saved to a file", "Close");
            }
            else
            {
                await CoreMethods.DisplayAlert("Please enter valid inputs", "Price & Weight", "Close");
            }
        }

        private async Task ShowPopup()
        {
            var totalAmount = CalculateTotalAmount();
            await CoreMethods.DisplayAlert("Your total amount for the gold purchased is" , totalAmount.ToString() , "Okay");
        }

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
    }
}
