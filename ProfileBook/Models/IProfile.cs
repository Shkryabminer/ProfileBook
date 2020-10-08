using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
   public interface IProfile
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string SecondName { get; set; }
        string Picture { get; set; }
        int UserID { get; set; }
        string Description { get; set; }

    }
}
