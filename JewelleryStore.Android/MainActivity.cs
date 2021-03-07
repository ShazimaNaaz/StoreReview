using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using System.Collections.Generic;

namespace JewelleryStore.Droid
{
    [Activity(Label = "JewelleryStore", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
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
                    parms.Add("Device", "AppVersion:" + crashReport.Device.AppVersion + ";OsApiLevel:" + crashReport.Device.OsApiLevel + ";Model:" + crashReport.Device.Model + ";OsVersion:" + crashReport.Device.OsVersion);
                    parms.Add("Id", crashReport.Id.ToString());
                    parms.Add("AndroidDetails.ThreadName", crashReport.AndroidDetails.ThreadName);

                    Crashes.TrackError(crashReport.Exception, parms);
                }
            });

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}