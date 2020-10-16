using ProfileBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;


namespace ProfileBook.Services
{
    public class UserRepository : IUserRepository
    {
        SQLiteConnection dataBase;
        string _dataPath;
        public string DataPath
        {
            get
            { if (_dataPath == null)
                    _dataPath = "Profiles.db";
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _dataPath); }
            set { _dataPath = value; }
        }
        public UserRepository(string dataPath)
        {
            DataPath = dataPath;
            dataBase = new SQLiteConnection(DataPath);
            dataBase.CreateTable<User>();
        }
        public UserRepository()
        {
            dataBase = new SQLiteConnection(DataPath);
            dataBase.CreateTable<User>();
        }
        public int DeleteUser(int id)
        {
           return dataBase.Delete<User>(id);
        }

        public IUser GetUser(int id)
        {
            var user = from u in GetUsers()
                       where u.ID == id
                       select u;
            return user.First();
        }

        public IEnumerable<User> GetUsers()
        {
            return dataBase.Table<User>().ToList();
        }

        public int SaveUser(IUser user)
        {          
          return  dataBase.Insert(user);
        }
    }
}
