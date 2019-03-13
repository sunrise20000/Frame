using Frame.Definations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Frame.Converts
{
    public class MsgType2Image : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            EnumMsgType msg = (EnumMsgType)value;
            BitmapImage bitmap = null;
            switch (msg)
            {
                case EnumMsgType.Info:
                    bitmap = new BitmapImage(new Uri(@"..\images\infomation.png", UriKind.Relative));
                    break;
                case EnumMsgType.Warning:
                    bitmap = new BitmapImage(new Uri(@"..\images\warning.png", UriKind.Relative));
                    break;
                case EnumMsgType.Error:
                    bitmap = new BitmapImage(new Uri(@"..\images\error.png", UriKind.Relative));
                    break;
                default:
                    break;
            }
            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
