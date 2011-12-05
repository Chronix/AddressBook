using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using AddressBook;

namespace AddressBookClient
{
    [ValueConversion(typeof(IMType), typeof(ImageSource))]
    public class IMTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IMType type = (IMType)value;
            Uri uri = null;

            switch (type)
            {
                case IMType.AIM: uri = new Uri("/res/aim.png", UriKind.Relative); break;
                case IMType.GTalk: uri = new Uri("/res/gtalk.png", UriKind.Relative); break;
                case IMType.ICQ: uri = new Uri("/res/icq.png", UriKind.Relative); break;
                case IMType.Jabber: uri = new Uri("/res/jabber.png", UriKind.Relative); break;
                case IMType.MSN: uri = new Uri("/res/msn.png", UriKind.Relative); break;
                case IMType.Yahoo: uri = new Uri("/res/yahoo.png", UriKind.Relative); break;
            }

            return new BitmapImage(uri);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
