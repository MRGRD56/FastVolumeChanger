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
                MiddleMouseButtonAction.None => Properties.Strings.MmbaNone,
                MiddleMouseButtonAction.Mute => Properties.Strings.MmbaMute,
                MiddleMouseButtonAction.OpenSettings => Properties.Strings.MmbaOpenSettings,
                MiddleMouseButtonAction.PlayPause => Properties.Strings.MmbaPlayPause,
                _ => throw new Exception()
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
