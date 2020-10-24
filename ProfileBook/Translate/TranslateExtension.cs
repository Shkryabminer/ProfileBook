using System;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;
using ProfileBook.Services;
using ProfileBook.Resources;

namespace ProfileBook.Translate
{
    [ContentProperty("Text")]    
    public class TranslateExtension : IMarkupExtension
    {
        
        private readonly CultureInfo ci;
        

        #region --Public properties--
        public string Text { get; set; }
        #endregion

        public TranslateExtension()
        {
          ci = Resource.Culture;
        }
       

        #region --Implementation IMarkUpExtension--
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var tmp = serviceProvider;
            if (Text == null)
                return "";

            ResourceManager resmgr = GetResourceManager();

            var translation = resmgr.GetString(Text, ci);

            if (translation == null)
            {
                translation = Text;
            }
            return translation;
        }
        #endregion

        #region --Private helpers--
        private ResourceManager GetResourceManager()
        {


            string resource = Constants._resource;

            return new ResourceManager(resource, typeof(TranslateExtension).GetTypeInfo().Assembly);
        }
        #endregion
    }
}
