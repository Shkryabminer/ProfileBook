

namespace ProfileBook.Helpers
{
    public class SettingsLoader :ISettingsLoader
    {
        private readonly IThemeLoader _themeLoader;
        private readonly ILocalizationLoader _localizationLoader;

        public SettingsLoader(IThemeLoader themeLoader,
            
            ILocalizationLoader localizationLoader)
        {
            _themeLoader = themeLoader;
            _localizationLoader = localizationLoader;
        }

        #region --ISettingLoader Implementation--

        public void LoadAppSettings()
        {
            _themeLoader.LoadUITheme();
            _localizationLoader.LoadLocalization();
        }

        #endregion
    }
}
