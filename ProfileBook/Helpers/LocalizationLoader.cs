using ProfileBook.Resources;
using ProfileBook.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace ProfileBook.Helpers
{
   public class LocalizationLoader:ILocalizationLoader
    {
        private readonly ISettingsManager _settingsManager;

        public LocalizationLoader(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        #region --ILocalizationLoader implementation--
        public void LoadLocalization()
        {
            var local = _settingsManager.LanguageSource;
            Resource.Culture = new System.Globalization.CultureInfo(local);
        }
        #endregion
    }
}
