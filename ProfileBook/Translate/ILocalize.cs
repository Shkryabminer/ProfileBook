using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ProfileBook.Translate
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
    }
}
