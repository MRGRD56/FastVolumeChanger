using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using FastVolumeFw.Classes;

namespace FastVolumeFw.Converters
{
    public class MiddleMbActionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var obj = (MiddleMouseButtonAction) value;
            return obj switch
            {
                MiddleMouseButtonAction.None => "None",
                MiddleMouseButtonAction.Mute => "Mute",
                MiddleMouseButtonAction.OpenSettings => "Open app settings",
                MiddleMouseButtonAction.PlayPause => "Play or pause",
                _ => throw new Exception()
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
