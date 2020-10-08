using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services
{
  public  interface IAuthentificationService
    {
    
        bool IsAutenficated(string login, string password);
        IUser GetAuthUser(string login, string password);

    }
}
