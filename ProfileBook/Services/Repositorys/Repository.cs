using ProfileBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProfileBook.Services
{
    public class Repository : IRepository
    {

        private readonly SQLiteConnection _dataBase;
        private string _dataPath;
        private string DataPath
        {
            get
            {
                if (_dataPath == null)
                    _dataPath = "Profiles.db";
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _dataPath);
            }
            set { _dataPath = value; }
        }
        public Repository(string dataPath) : this()
        {
            DataPath = dataPath;                        
        }
        public Repository()
        {           
            _dataBase = new SQLiteConnection(DataPath);
            _dataBase.CreateTable<User>();
            _dataBase.CreateTable<Profile>();
         //   InitTable<User>();
         //   InitTable<Profile>();
        }
        #region --Implement Irepository--
        public int DeleteItem<T>(T item) where T : IItem, new()
        {

            return _dataBase.Delete<T>(item.ID);
        }        

        public IEnumerable<T> GetItems<T>() where T : IItem, new()
        {
            return _dataBase.Table<T>();
        }

        public int SaveItem<T>(T item) where T : IItem, new()
        {
            if (item.ID != 0)
            {
                _dataBase.Update(item);
                return item.ID;
            }
            else
            {
                return _dataBase.Insert(item);
            }
        }
        #endregion

        #region --Private helpers--
        private void InitTable<T>() where T : IItem, new()
        {
            _dataBase.CreateTable<T>();
        }
        #endregion


    }
}

