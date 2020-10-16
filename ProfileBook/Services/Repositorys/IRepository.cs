using System;
using System.Collections.Generic;
using System.Text;
using ProfileBook.Models;
using SQLite;

namespace ProfileBook.Services
{
    public interface IRepository 
    {
      
        IEnumerable<T> GetItems<T>() where T : IItem, new();
        //  T GetItem(int id);
       // void InitTable<T>() where T : IItem, new();
        int DeleteItem<T>(T item) where T : IItem, new();
        int SaveItem<T>(T item) where T : IItem, new();
    }
}
