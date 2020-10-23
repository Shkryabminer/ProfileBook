﻿using System;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;
using ProfileBook.Services;



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
          ci = new CultureInfo(1033);
        }
        public TranslateExtension(ISettingsManager manager):this()
        {
            _settingsManger = manager;           
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
