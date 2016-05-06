using System;
using System.Windows.Media;

namespace HairDress.VOL
{
    public class Hsla
    {
        public decimal H, S, L, A;
        public static implicit operator Color(Hsla color)
        {
            decimal r, g, b, a = color.A;
            if (color.S == 0)
            {
                r = g = b = 1;
            }
            else
            {
                Func<decimal, decimal, decimal, decimal> hue2Rgb = (p, q, t) =>
                {
                    if (t < 0) t += 1;
                    if (t > 1) t -= 1;
                    if (t < 1 / 6m) return p + (q - p) * 6 * t;
                    if (t < 1 / 2m) return q;
                    if (t < 2 / 3m) return p + (q - p) * (2 / 3m - t) * 6;
                    return p;
                };
                var q2 = color.L < 0.5m ? color.L * (1 + color.S) : color.L + color.S - color.L * color.S;
                var p2 = 2 * color.L - q2;
                r = hue2Rgb(p2, q2, color.H + 1 / 3m);
                g = hue2Rgb(p2, q2, color.H);
                b = hue2Rgb(p2, q2, color.H - 1 / 3m);
            }
            return new Color { A = (byte)Math.Round(a * 255), R = (byte)Math.Round(r * 255), G = (byte)Math.Round(g * 255), B = (byte)Math.Round(b * 255) };
        }
        public static implicit operator Hsla(Color color)
        {
            decimal a = color.A / 255m;
            decimal r = color.R / 255m;
            decimal g = color.G / 255m;
            decimal b = color.B / 255m;
            var max = Math.Max(Math.Max(r, g), b);
            var min = Math.Min(Math.Min(r, g), b);
            var hsl = new Hsla { A = a, L = (max + min) / 2m };

            if (max == min)
            {
                hsl.H = hsl.S = 0;
            }
            else
            {
                var d = max - min;
                hsl.S = hsl.L > 0.5m ? d / (2 - max - min) : d / (max + min);
                if (max == r)
                {
                    hsl.H = (g - b) / d + (g < b ? 6 : 0);
                }
                else if (max == g)
                {
                    hsl.H = (b - r) / d + 2;
                }
                else if (max == b)
                {
                    hsl.H = (r - g) / d + 4;
                }
                hsl.H /= 6;
            }
            return hsl;
        }

        public static implicit operator Hsla(SolidColorBrush color)
        {
            return color.Color;
        }
        public static implicit operator SolidColorBrush(Hsla color)
        {
            return new SolidColorBrush(color);
        }
    }
}