using System;
using JewelleryStore.CustomRenderers;
using JewelleryStore.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderLessEntry), typeof(BorderLessEntryRenderer))]
namespace JewelleryStore.iOS.CustomRenderers
{
    public class BorderLessEntryRenderer : EntryRenderer
    {
        /// <summary>
        /// OnElementChanged
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null && Element != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
                Control.ReturnKeyType = UIReturnKeyType.Done;
            }
        }
    }
}
