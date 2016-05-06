using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Media;
using HairDress.VOL;

namespace HairDress.PL.Converter
{
    public class ColorHsl : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convert = parameter?.ToString().Trim(' ');
            if (string.IsNullOrEmpty(convert)) { return value; }

            var reg = new Regex(@"(H(?<H>\d{1,3}))?(S(?<S>\d{1,3}))?(L(?<L>\d{1,3}))?(A(?<A>\d{1,3}))?", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            var matches = reg.Matches(convert);

            if (value is Color)
            {
                Group grp;
                var hsla = (Hsla)value;
                if ((grp = matches[0].Groups["H"]).Success) { hsla.H = int.Parse(grp.Value) / 100m; }
                if ((grp = matches[0].Groups["S"]).Success) { hsla.S = int.Parse(grp.Value) / 100m; }
                if ((grp = matches[0].Groups["L"]).Success) { hsla.L = int.Parse(grp.Value) / 100m; }
                if ((grp = matches[0].Groups["A"]).Success) { hsla.A = int.Parse(grp.Value) / 100m; }
                return (Color)hsla;
            }

            if (value is SolidColorBrush)
            {
                Group grp;
                var hsla = (Hsla)(SolidColorBrush)value;
                if ((grp = matches[0].Groups["H"]).Success) { hsla.H = int.Parse(grp.Value) / 100m; }
                if ((grp = matches[0].Groups["S"]).Success) { hsla.S = int.Parse(grp.Value) / 100m; }
                if ((grp = matches[0].Groups["L"]).Success) { hsla.L = int.Parse(grp.Value) / 100m; }
                if ((grp = matches[0].Groups["A"]).Success) { hsla.A = int.Parse(grp.Value) / 100m; }
                return (SolidColorBrush)hsla;
            }

            if (value is GradientBrush)
            {
                var gto = matches.Count - 1 > 1;
                for (var i = 0; i < ((GradientBrush)value).GradientStops.Count; i++)
                {
                    Group grp;
                    var hsla = (Hsla)((GradientBrush)value).GradientStops[i].Color;
                    if ((grp = matches[gto ? i : 0].Groups["H"]).Success) { hsla.H = int.Parse(grp.Value) / 100m; }
                    if ((grp = matches[gto ? i : 0].Groups["S"]).Success) { hsla.S = int.Parse(grp.Value) / 100m; }
                    if ((grp = matches[gto ? i : 0].Groups["L"]).Success) { hsla.L = int.Parse(grp.Value) / 100m; }
                    if ((grp = matches[gto ? i : 0].Groups["A"]).Success) { hsla.A = int.Parse(grp.Value) / 100m; }
                    ((GradientBrush)value).GradientStops[i].Color = hsla;
                }
                return (GradientBrush)value;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}