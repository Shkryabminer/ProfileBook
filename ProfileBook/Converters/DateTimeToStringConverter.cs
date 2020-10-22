using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProfileBook.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        #region --IvalueConverter Implementation--
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          return ((DateTime)value).ToString("MM/dd/yyyy hh.mm tt");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Now.ToString();
        }
        #endregion
    }
}
