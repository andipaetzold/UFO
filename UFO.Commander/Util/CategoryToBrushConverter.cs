namespace UFO.Commander.Util
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class CategoryToBrushConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
            var bytes = BitConverter.GetBytes(value.GetHashCode());
            return new SolidColorBrush(Color.FromRgb(bytes[0], bytes[1], bytes[2]));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
