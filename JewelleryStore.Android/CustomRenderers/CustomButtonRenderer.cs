using System;
using JewelleryStore.CustomRenderers;
using JewelleryStore.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete
namespace JewelleryStore.Droid.CustomRenderers
{
    [Obsolete]
    public class CustomButtonRenderer :ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if(Control!=null)
            {
                Control.SetAllCaps(false);
            }
        }
    }

  
}
