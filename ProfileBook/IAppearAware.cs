using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook
{
   public interface IAppearAware
    {
        void OnAppearing();
        void OnDisAppearing();
    }
}
