using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDB.Models;

namespace TMDB.Controls.Converters
{
    public class GenreToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {               
            var genres = value as List<Genre>;
            if (genres == null) return null;

            var  names = genres.Select(g => g.Name).ToList();
            
            return string.Join(", ", names);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
