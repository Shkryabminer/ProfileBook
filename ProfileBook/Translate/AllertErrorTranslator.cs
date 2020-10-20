using ProfileBook.Resources;
using ProfileBook.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;

namespace ProfileBook.Translate
{
    public class AllertErrorTranslator : ITRanslate
    {
        #region --Properties--
        private readonly ISettingsManager _settingsManager;
        private ResourceManager res;
        #endregion
        public AllertErrorTranslator(ISettingsManager manager)
        {
            _settingsManager = manager;            
        }

        #region -- ITranslate Implementation --
        public string GetTranslate(string key)
        {
            res = new ResourceManager(_settingsManager.LanguageSource, Assembly.GetExecutingAssembly());
            return res.GetString(key);
        }
        #endregion
    }
}
