using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ProfileBook.Localization
{
   public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
    }
}
