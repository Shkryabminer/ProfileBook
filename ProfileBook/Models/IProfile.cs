using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
   public interface IProfile:IItem
    {
      //  int ID { get; set; }
        string FirstName { get; set; }
        string SecondName { get; set; }
        string Picture { get; set; }
        int UserID { get; set; }
        string Description { get; set; }

    }
}
