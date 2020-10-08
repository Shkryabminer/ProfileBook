using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ProfileBook.Models;

namespace ProfileBook.Services
{
    public  class AutentificationService : IAuthentificationService
    {
      public bool IsAutenficated(string login, string password)
        {
            var c = from u in UsersReposytory.GetUsers()
                    where u.Login == login && u.Password == password
                    select u;
            return (c != null&&c.Count<User>()>0);
        }
       public IUserRepository UsersReposytory { get; set; }
        public AutentificationService(IUserRepository userReposytory)
        {
            UsersReposytory = userReposytory;
        }
        public IUser GetAuthUser(string login, string password)
        {
            IUser user=null;
            if(IsAutenficated(login,password))
            {
                var c = from u in UsersReposytory.GetUsers()
                        where u.Login == login && u.Password == password
                        select u;
                user = c.First<User>();

            }
            return user;
        }
    }
}
