using ProfileBook.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services
{
 public interface IProfileRepository
    {
     //   int UserId { get;  set; }
        IEnumerable<Profile> GetUserContacts(int userId);
        Profile GetContact(int id);
        void DeleteContact(int id);
        int SaveContact(IProfile contact);
    }
}
