using Auxilia.Extensions;
using System;

namespace Auxilia.Imaging
{
	public struct Color
    {
        public Color(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }
        public Color(byte r, byte g, byte b)
            : this(255, r, g, b)
        {
        }
        private Color(float r, float g, float b)
            : this(255, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255))
        {
        }

        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public Hsb Hsb
        {
            get => Hsb.FromColor(this);
            set
            {
                A = value.Color.A;
                R = value.Color.R;
                G = value.Color.G;
                B = value.Color.B;
            }
        }
        public Hsl Hsl
        {
            get => Hsl.FromColor(this);
            set
            {
                A = value.Color.A;
                R = value.Color.R;
                G = value.Color.G;
                B = value.Color.B;
            }
        }

        public string ColorCode
        {
            get => $"#{A:X2}{R:X2}{G:X2}{B:X2}";
        }

        public static Color FromArgb(byte a, byte r, byte g, byte b)
        {
            return new Color(a, r, g, b);
        }
        public static Color FromRgb(byte r, byte g, byte b)
        {
            return new Color(r, g, b);
        }

        public static Color FromHue(float hue)
        {
            return new Color(
                Math.Abs(hue * 6 - 3) -1,
                2 - Math.Abs(hue * 6 - 2),
                2 - Math.Abs(hue * 6 - 4));
        }

        public static Color FromHsb(float hue, float saturation, float brightness)
        {
            hue = hue.Clamp(0, 1) * 360;
            saturation = saturation.Clamp(0, 1);
            brightness = brightness.Clamp(0, 1);

            float chroma = brightness * saturation;
            float x = chroma * (1 - Math.Abs(hue / 60 % 2 - 1));

            Color color = hue switch
            {
                { } h when h < 60 => new Color(chroma, x, 0),
                { } h when h < 120 => new Color(x, chroma, 0),
                { } h when h < 180 => new Color(0, chroma, x),
                { } h when h < 240 => new Color(0, x, chroma),
                { } h when h < 300 => new Color(x, 0, chroma),
                _ => new Color(chroma, 0, x)
            };

            byte m = (byte)((brightness - chroma) * 255);

            color.R += m;
            color.G += m;
            color.B += m;
            return color;
        }
        public static Color FromHsl(float hue, float saturation, float lightness)
        {
            hue = hue.Clamp(0, 1) * 360;
            saturation = saturation.Clamp(0, 1);
            lightness = lightness.Clamp(0, 1);

            float chroma = (1 - Math.Abs(2 * lightness - 1)) * saturation;
            float x = chroma * (1 - Math.Abs(hue / 60 % 2 - 1));

            Color color = hue switch
            {
                { } h when h < 60 => new Color(chroma, x, 0),
                { } h when h < 120 => new Color(x, chroma, 0),
                { } h when h < 180 => new Color(0, chroma, x),
                { } h when h < 240 => new Color(0, x, chroma),
                { } h when h < 300 => new Color(x, 0, chroma),
                _ => new Color(chroma, 0, x)
            };

            byte m = (byte)((lightness - chroma / 2) * 255);

            color.R += m;
            color.G += m;
            color.B += m;
            return color;
        }

        public static Color FromColor(System.Drawing.Color color)
        {
            return new Color(color.A, color.R, color.G, color.B);
        }
        public static Color FromColor(System.Windows.Media.Color color)
        {
            return new Color(color.A, color.R, color.G, color.B);
        }
    
        public System.Drawing.Color ToDrawingColor()
        {
            return System.Drawing.Color.FromArgb(A, R, G, B);
        }
        public System.Drawing.Color FromDrawingColor()
        {
            return System.Drawing.Color.FromArgb(A, R, G, B);
        }

        public override string ToString()
        {
            return $"{ColorCode} (A={A}, R={R}, G={G}, B={B})";
        }
    }
}
