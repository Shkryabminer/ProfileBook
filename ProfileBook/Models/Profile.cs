using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProfileBook.Models
{[Table("Profiles")]
    public class Profile : IProfile

    {[AutoIncrement,PrimaryKey,Column("ID")]
        public int Id { get ; set ; }
        public string FirstName { get ; set ; }
        public string SecondName { get ; set ; }
        public string Picture { get ; set ; }
        public int UserID { get; set ; }
        public string Description { get; set; }
        public Profile()
        {
            FirstName = "";
            SecondName = "";
            Picture = "";
            Description = "";
        }
        public Profile(int userId):this()
        {
            UserID = userId;
        }

    }

}
