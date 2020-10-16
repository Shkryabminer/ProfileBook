using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ProfileBook.Models;

namespace ProfileBook.Services
{
    public  class AutentificationService : IAuthentificationService
    {
     
      // public IUserRepository UsersReposytory { get; set; }
        public IRepository Data { get; set; }
        //public AutentificationService(IUserRepository userReposytory)
        //{
        //    UsersReposytory = userReposytory;
        //}
        public AutentificationService(IRepository repository)
        {
            Data = repository;
        }
        public IUser GetAuthUser(string login, string password)
        {
            IUser user=null;
            if(IsAutenficated(login,password))
            {
                //var c = from u in UsersReposytory.GetUsers()
                var c = from u in Data.GetItems<User>()
                        where u.Login == login && u.Password == password
                        select u;
                user = c.First();
            }
            return user;
        }
        public bool IsAutenficated(string login, string password)
        {
            //var c = from u in UsersReposytory.GetUsers()
            var c = from u in Data.GetItems<User>() 
                    where u.Login == login && u.Password == password
                    select u;
            return (c != null && c.Count() > 0);
        }
    }
}
