using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Autorization
{
   public interface IAutorization
    {
        bool Autorizeted();
        void LogOut();
        int GetActiveUser();
        void SetActiveUser(int id);

    }
}
