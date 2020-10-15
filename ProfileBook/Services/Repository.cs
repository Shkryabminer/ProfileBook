using ProfileBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProfileBook.Services
{
    public class Repository : IRepository //where T : IItem,new()//, IUser
    {

        SQLiteConnection dataBase;
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
        public Repository(string dataPath):this()
        {
            DataPath = dataPath;
            //dataBase = new SQLiteConnection(DataPath);
            //dataBase.CreateTable<T>();
        }
        public Repository()
        {
            dataBase = new SQLiteConnection(DataPath);
        }
        public void TableInit<T>() where T : IItem, new()
        {            
            dataBase.CreateTable<T>();
        }
        public int DeleteItem<T>(T item)  where T : IItem, new()
        {
             
            return dataBase.Delete<T>(item.ID);
        }

        //public T GetItem(int id)
        //{
        //    return user.First();
        //    throw new NotImplementedException();
        //}

        public IEnumerable<T> GetItems<T>() where T : IItem, new()
        {
            return dataBase.Table<T>().ToList();
        }

        public int SaveItem<T>(T item) where T : IItem, new()
        {
            if (item.ID != 0)
            {
                dataBase.Update(item);
                return item.ID;
            }
            else
            {
                return dataBase.Insert(item);
            }
        }

        //public int DeleteItem<T>(int id) where T : IItem, new()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
