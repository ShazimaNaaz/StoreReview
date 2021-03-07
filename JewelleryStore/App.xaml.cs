using System;
using FreshMvvm;
using JewelleryStore.Logging;
using JewelleryStore.PageModels;
using JewelleryStore.Pages;
using JewelleryStore.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JewelleryStore
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Inversion of control
            FreshIOC.Container.Register<IloggerService, LoggerService>();
            FreshIOC.Container.Register<ILoginService, LoginService>();

            // Setting up of initial page
            var page = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page);
            MainPage = basicNavContainer;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static void DisplayUIAlert(MessageObject message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Current.MainPage.DisplayAlert(message.Title, message.Message, message.OkButtonText);
            });
        }
    }
}
