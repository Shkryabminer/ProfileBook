using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;
using ProfileBook.Services;
using System.ComponentModel;
using ProfileBook.Localization;

namespace ProfileBook.Translate
{
    [ContentProperty("Text")]    
    public class TranslateExtension : IMarkupExtension
    {
        public static string ResourceId { get; set; }
        private readonly CultureInfo ci;
        private static  ISettingsManager _settingsManger;
        #region --Public properties--
        public string Text { get; set; }
        #endregion
        public TranslateExtension()
        {
            //ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            ci = new CultureInfo(1033);
        }
        public TranslateExtension(ISettingsManager manager):this()
        {
            _settingsManger = manager;
           // ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            //ci = new CultureInfo(1033);
        }

        #region --Implementation IMarkUpExtension--
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var tmp = serviceProvider;
            if (Text == null)
                return "";

            ResourceManager resmgr = new ResourceManager(_settingsManger.LanguageSource,
                        typeof(TranslateExtension).GetTypeInfo().Assembly);

            var translation = resmgr.GetString(Text, ci);

            if (translation == null)
            {
                translation = Text;
            }
            return translation;
        }
        #endregion
    }}
