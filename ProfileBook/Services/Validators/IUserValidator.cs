using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Validators
{
    public interface IUserValidator
    {
        bool IsValid(IUserRepository userRepository);
    }
}
