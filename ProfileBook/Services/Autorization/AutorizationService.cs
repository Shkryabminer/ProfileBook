using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Autorization
{
    public class AutorizationService : IAutorization
    {
        readonly string key = "Id";

        public AutorizationService()
        {
        }
        public bool Autorizeted()
        {
            return (GetActiveUser() > -1);
        }
        public void LogOut()
        {
            CrossSettings.Current.AddOrUpdateValue(key, -1);
        }
        public int GetActiveUser()
        {
            return CrossSettings.Current.GetValueOrDefault(key, -1);
        }
        public void SetActiveUser(int id)
        {
            CrossSettings.Current.AddOrUpdateValue(key, id);
        }
    }
}
