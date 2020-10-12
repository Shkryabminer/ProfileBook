using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProfileBook.Models
{[Table("Profiles")]
    public class Profile : IProfile

    {[AutoIncrement,PrimaryKey,Column("ID")]
        public int Id { get ; set ; }
        public String FirstName { get ; set ; }
        public String SecondName { get ; set ; }
        public string Picture { get; set; }
        public int UserID { get; set ; }
        public string Description { get; set; }
        public Profile()
        {
            FirstName = "";
            SecondName = "";
            Picture = "";
            Description = "";
            Picture = "pic_profile.png";
        }
        public Profile(int userId):this()
        {
            UserID = userId;
        }

    }

}
