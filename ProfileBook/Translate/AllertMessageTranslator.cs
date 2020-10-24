using ProfileBook.Resources;
using System.Reflection;
using System.Resources;

namespace ProfileBook.Translate
{
    public class AllertMessageTranslator : ITRanslate
    {       
        public AllertMessageTranslator()
        {
        }

        #region -- ITranslate Implementation --
        public string GetTranslate(string key)
        {
            ResourceManager res = GetResourceManager();
            string mes = res.GetString(key, Resource.Culture);
            return mes;
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
