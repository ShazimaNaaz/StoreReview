using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using FreshMvvm;
using JewelleryStore.Common;
using JewelleryStore.Logging;
using JewelleryStore.Models;
using JewelleryStore.Pages;
using JewelleryStore.Service;
using PropertyChanged;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JewelleryStore.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class LoginPageModel : FreshBasePageModel
    {
        #region properties, fields
        IloggerService _logger;
        ILoginService _loginAuth;

        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }

        public string CloseConstant = Helper.Resources.StringResources.CloseConstant;
        public string LoginErrorConstant = Helper.Resources.StringResources.LoginErrorConstant;
        public string LoginErrorMessageConstant = Helper.Resources.StringResources.LoginErrorMessageConstant;
        #endregion
        public LoginPageModel(IloggerService loggerService, ILoginService loginService)
        {
            _logger = loggerService;
            _loginAuth = loginService;
        }

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    try
                    {
                        if (!String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Password))
                        {
                            var result = _loginAuth.ValidateAuthentication(UserName, Password).Result;
                            if (result != null)
                            {
                                if (result.Item1)
                                {
                                    UserType = result.Item2;
                                    await CoreMethods.PushPageModel<EstimationPageModel>(result.Item2);
                                }
                                else
                                {
                                    await CoreMethods.DisplayAlert(LoginErrorConstant, LoginErrorMessageConstant, CloseConstant);
                                }
                            }
                        }
                       
                        
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogRecord("Error in LoginPageModel  : LoginCommand : Param = " + param, LogType.Error, ex);
                    }
                });
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new Command(async (param) =>
                {
                    try
                    {
                        UserName = string.Empty;
                        Password = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        await _logger.LogRecord("Error in LoginPageModel  : CancelCommand : Param = " + param, LogType.Error, ex);
                    }
                });
            }
        }
        #endregion
    }
}
