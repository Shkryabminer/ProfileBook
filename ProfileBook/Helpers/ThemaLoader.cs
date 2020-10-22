using ProfileBook.Services;
using ProfileBook.Themes;

using System.Collections.Generic;

using Xamarin.Forms;

namespace ProfileBook.Helpers
{
    public class ThemaLoader : IThemeLoader
    {
        private readonly ISettingsManager _settingsManager;
        public ThemaLoader(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        #region --IThemeLoader Implementation--
        public void LoadUITheme()
        {
            ICollection<ResourceDictionary> mergeDictionaries = Application.Current.Resources.MergedDictionaries;
            mergeDictionaries.Clear();
            if (_settingsManager.ThemaSource == nameof(DarkThema))
                mergeDictionaries.Add(new DarkThema());
            else mergeDictionaries.Add(new LightThema());
        }
        #endregion
    }
}
