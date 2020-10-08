using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace ProfileBook.Models
{
    [Table("Users")]
    public class User : IUser
    {
        [AutoIncrement,PrimaryKey,Column("UserID")]
        public int UserID { get ; set; }
        public string Login { get; set ; }
        public string Password { get ; set ; }
        public User()
        { }
        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
