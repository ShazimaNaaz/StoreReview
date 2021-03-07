using System;
using Android.Content;
using JewelleryStore.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderLessEntry), typeof(BorderLessEntryRenderer))]
public class BorderLessEntryRenderer : EntryRenderer
{
    public BorderLessEntryRenderer(Context context) : base(context)
    {
    }
    protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
    {
        base.OnElementChanged(e);
        if (Control != null)
        {
            Control.Background = null;
            var entry = (BorderLessEntry)e.NewElement;
            try
            {
                Control.ScrollBarSize = 0;
            }
            catch (Exception ex)
            {
                //var loggerService = FreshMvvm.FreshIOC.Container.Resolve<Services.Interfaces.Logging.ILoggerService>();
                //loggerService.LogRecord("Error in JewelleryStore.Droid.CustomRenderers.BorderLessEntryRenderer.cs in OnElementChanged", Services.Interfaces.Logging.LogType.Error, ex);
            }
        }
    }
}
