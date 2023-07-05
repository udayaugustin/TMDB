using System.Globalization;

namespace TMDB.Converters
{
    public class ImagePathConverter : IValueConverter
    {
        const string basePath = "https://image.tmdb.org/t/p";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            
            string imagePath = value.ToString();

            return $"{basePath}/{parameter}/{imagePath}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
