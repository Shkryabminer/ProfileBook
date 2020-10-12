using System;
using System.Collections.Generic;
using System.Text;
using ProfileBook.Models;

namespace ProfileBook.Services.Validators
{
    public interface IPasswordValidator
    {
        string IsValid(string login, string password, string confirm, IEnumerable<User> users);
    }
}
