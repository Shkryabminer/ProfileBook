using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IRepository _repository;
        public ProfileService(ISettingsManager settingsManager, IRepository repository)
        {
            _settingsManager = settingsManager;
            _repository = repository;
        }
        #region --IProfileService impement--
        public void DeleteProfile(Profile prof)
        {
            _repository.DeleteItem(prof);
        }

        public List<Profile> GetProfiles(int id)
        {
            if (_settingsManager.SelectedSortMethode == 1)
                return GetOrderedByName(id);
            else if (_settingsManager.SelectedSortMethode == 2)
                return GetOrderedByNick(id);
            else return GetOrderedByDate(id);
        }

        public void SaveOrUpdateProfile(Profile profile)
        {
            _repository.SaveItem(profile);
        }
        #endregion

        #region --Private helpers--
        private List<Profile> GetOrderedByDate(int id) 
        {
            return _repository.GetItems<Profile>().Where(x=>x.UserID==id).OrderBy(x => x.Date).ToList();
        }
        private List<Profile> GetOrderedByName(int id)
        {
            return _repository.GetItems<Profile>().Where(x => x.UserID == id).OrderBy(x => x.FirstName).ToList();
        }
        private List<Profile> GetOrderedByNick(int id)
        {
            return _repository.GetItems<Profile>().Where(x => x.UserID == id).OrderBy(x => x.SecondName).ToList();
        }
        #endregion
    }
}
