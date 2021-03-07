using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JewelleryStore.Helper.Resources
{
    [ContentProperty("Text")]
    public class ValuesExtensions : IMarkupExtension
    {
        const string ResourceId = "JewelleryStore.Helper.Resources.StringResource";
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;
            ResourceManager resourceManager = new ResourceManager(ResourceId, typeof(ValuesExtensions).GetTypeInfo().Assembly);

            return resourceManager.GetString(Text, CultureInfo.CurrentCulture);
        }
    }
}
