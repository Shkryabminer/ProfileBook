﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using ProfileBook.Models;
using ProfileBook.Translate;

namespace ProfileBook.Services.Validators
{
   public class PasswordValidator : IPasswordValidator
    {            
        public string IsValid(string login, string password, string confirm, IEnumerable<User> users)
        {
            string mess = "Valid";
            foreach (IUser user in users)
                if (user.Login == login)
                     mess= "LoginIsAlreadyTaken";
                else if (login.Length < 4 || login.Length > 16)
                    mess ="IncorrectLoginLength";
                else if (password.Length < 8 || password.Length > 16)
                    mess = "IncorrectPasswordLength";
                else if (password != confirm)
                    mess = "PassConfirmMis";
                else if (!password.Any(x => char.IsLower(x)))
                 mess = "PassNotContainLet"; 
                else if (!password.Any(x => char.IsUpper(x)))
                    mess = "PassNotContainUp"; 
                else if (password.Contains(" ")||login.Contains(" "))
                    mess ="LoginPasswordContainSpace";
                else
                {
                    string pattern = @"\d{1}\w*";
                    bool mis = Regex.IsMatch(login, pattern);
                    if (mis)
                        mess = "LoginStartFromNumber";
                }
            return mess;
        }
    }
}
