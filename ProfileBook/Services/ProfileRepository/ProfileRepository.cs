using ProfileBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace ProfileBook.Services
{
    public class ProfileRepository : IProfileRepository
    {
      //  public int UserId { get ; protected set; }

        private SQLiteConnection database;
        string _dataPath;
        public string DataPath
        {
            get
            {
                if (_dataPath == null)
                    _dataPath = "Profiles.db";
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _dataPath);
            }
            set { _dataPath = value; }
        }

        public ProfileRepository(int userId, string dataPath)
        {
            DataPath = dataPath;
            database = new SQLiteConnection(DataPath);
            database.CreateTable<Profile>();
           // UserId = userId;
        }
        public ProfileRepository()
        {
            database = new SQLiteConnection(DataPath);
            database.CreateTable<Profile>();
        }


        public int SaveContact(IProfile contact)
        {
            if (contact.ID != 0)
            {
                database.Update(contact);
                return contact.ID;
            }
            else
            {
                return database.Insert(contact);
            }
        }

        public void DeleteContact(int id)
        {
            database.Delete<Profile>(id);
        }

        public IEnumerable<Profile> GetUserContacts(int userId)
        {
            List<Profile> contacts = GetAll();
            var query = from c in contacts
                        where c.UserID == userId
                        select c;
            return query;
        }
        private List<Profile> GetAll()
        {
            return database.Table<Profile>().ToList();
        }
        public Profile GetContact(int id)
        {
           return database.Get<Profile>(id);
        }
    }
}
