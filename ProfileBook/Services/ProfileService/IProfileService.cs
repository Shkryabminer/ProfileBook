using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.ProfileService
{
  public interface IProfileService
    {
        List<Profile> GetProfiles(int id);
        void DeleteProfile(Profile prof);
        void SaveOrUpdateProfile(Profile profile);

    }
}
