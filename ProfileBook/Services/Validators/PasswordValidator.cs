using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ProfileBook.Models;

namespace ProfileBook.Services.Validators
{
   public class PasswordValidator : IPasswordValidator
    {

        public string IsValid(string login, string password, string confirm, IEnumerable<User> users)
        {

            foreach (IUser user in users)
                if (user.Login == login)
                    return "This login is already taken";
                else if (login.Length < 4 || login.Length > 16)
                    return "Login have to be min 4 signs and less then 16";
                else if (password.Length < 8 || password.Length > 16)
                    return "Password have to be min 8 signs and less then 16";
                else if (password != confirm)
                    return "Password and Confirm missmatch";
                else {
                    string pattern = @"\d{1}\w*";
                    bool mis = Regex.IsMatch(login, pattern);
                    if (mis)                                 
                    return "Login have not start from number";
                  }

            return "Valid";
        }
    }
}
