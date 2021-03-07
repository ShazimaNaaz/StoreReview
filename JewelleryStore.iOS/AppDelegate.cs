using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Microsoft.AppCenter.Crashes;
using UIKit;

namespace JewelleryStore.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                Crashes.TrackError(ex);
            };

            Task.Run(async () =>
            {
                bool didAppCrash = await Crashes.HasCrashedInLastSessionAsync();
                if (didAppCrash)
                {
                    ErrorReport crashReport = await Crashes.GetLastSessionCrashReportAsync();
                    var parms = new Dictionary<string, string>();
                    parms.Add("AppErrorTime", crashReport.AppErrorTime.ToString());
                    parms.Add("Id", crashReport.Id.ToString());
                    parms.Add("iOSDetails.ExceptionName", crashReport.iOSDetails.ExceptionName);
                    parms.Add("iOSDetails.ExceptionReason", crashReport.iOSDetails.ExceptionReason);
                    parms.Add("iOSDetails.Signal", crashReport.iOSDetails.Signal);
                    parms.Add("iOSDetails.ReporterKey", crashReport.iOSDetails.ReporterKey);

                    Crashes.TrackError(crashReport.Exception, parms);
                }
            });
            return base.FinishedLaunching(app, options);
        }
    }
}
