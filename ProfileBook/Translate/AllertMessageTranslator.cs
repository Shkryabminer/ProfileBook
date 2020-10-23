using ProfileBook.Resources;
using ProfileBook.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;

namespace ProfileBook.Translate
{
    public class AllertMessageTranslator : ITRanslate
    {
        
        private readonly ISettingsManager _settingsManager;
        private ResourceManager res;
        
        public AllertMessageTranslator(ISettingsManager manager)
        {
            _settingsManager = manager;
        }

        #region -- ITranslate Implementation --
        public string GetTranslate(string key)
        {
            string source = GetPrepearedResource();
            res = new ResourceManager(source, Assembly.GetExecutingAssembly());
            return res.GetString(key);
        }

        #endregion

        #region --Private helpers--
        private string GetPrepearedResource()
        {
            string source;
            if (_settingsManager.LanguageSource == "en")
            {
                source = "ProfileBook.Resources.Resource";
            }
            else
            {
                source = "ProfileBook.Resources.Resource." + _settingsManager.LanguageSource;
            }
            return source;
        }
        #endregion

    }
}
