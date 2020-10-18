using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProfileBook.Models
{
    [Table("Profiles")]
    public class Profile : IProfile

    {
        [AutoIncrement, PrimaryKey, Column("ID")]
        public  int ID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        string _picture;
        public string Picture
        {
            get
            {
                if (_picture == null)
                    _picture = "pic_profile.png";
                return _picture;
            }
            set { _picture = value; }
        }
        public int UserID { get; set; }
        public string Description { get; set; }      
        public DateTime Date { get; set; }
        
        public Profile()
        {
            FirstName = "";
            SecondName = "";
            Description = "";            
        }
        public Profile(int userId) : this()
        {
            UserID = userId;
        }

    }

}
