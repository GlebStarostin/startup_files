using System;
using System.Globalization;
using System.Windows.Data;
using StartupFiles.Models;

namespace StartupFiles.Converters
{
    internal class StartupTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var startupType = (StartupType)value;
            switch (startupType)
            {
                case StartupType.Registry:
                    return "Registry";
                case StartupType.StartMenu:
                    return "Start Menu";
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
