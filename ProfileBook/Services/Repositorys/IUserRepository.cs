using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services
{
   public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        int SaveUser(IUser user);

        int DeleteUser(int id);

        IUser GetUser(int id);
    }
}
