using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
   public interface IUser
    {
        int UserID { get; set; }
        string Login { get; set; }
        string Password { get; set; }

    }
}
