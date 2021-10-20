using Auxilia.Extensions;
using System;
using System.Linq;

namespace Auxilia.Imaging
{
	public struct Hsl
    {
        private Color _color;

        private float _hue;
        private float _saturation;
        private float _lightness;

        public Hsl(float hue, float saturation, float lightness)
        {
            _color = Color.FromHsl(hue, saturation, lightness);

            _hue = hue.Clamp(0, 1);
            _saturation = saturation.Clamp(0, 1);
            _lightness = lightness.Clamp(0, 1);
        }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;

                Hsl hsl = FromColor(value);
                _hue = hsl._hue;
                _saturation = hsl._saturation;
                _lightness = hsl._lightness;
            }
        }

        public float Hue
        {
            get => _hue;
            set => Color = Color.FromHsb(value, _saturation, _lightness);
        }
        public float Saturation
        {
            get => _saturation;
            set => Color = Color.FromHsb(_hue, value, _lightness);
        }
        public float Lightness
        {
            get => _lightness;
            set => Color = Color.FromHsb(_hue, _saturation, value);
        }

        public static Hsl FromColor(Color color)
        {
            float[] values =
            {
                color.R / 255f,
                color.G / 255f,
                color.B / 255f
            };

            float minValue = values.Min();
            float maxValue = values.Max();
            float chroma = maxValue - minValue;

            float hue;

            if (chroma.Equals(0))
            {
                hue = 0;
            }
            else
            {
                hue = values[0].Equals(maxValue)
                    ? (values[1] - values[2]) / chroma
                    : values[1].Equals(maxValue)
                        ? 2 + (values[2] - values[0]) / chroma
                        : 4 + (values[0] - values[1]) / chroma;

                hue /= 6;
            }

            float lightness = (minValue + maxValue) / 2;

            float saturation = lightness.Equals(0) || lightness.Equals(1)
                ? 0
                : chroma / (1 - Math.Abs(2 * maxValue - chroma - 1));

            return new Hsl(hue, saturation, lightness);
        }
        public static Hsl FromColor(System.Drawing.Color color)
        {
            return FromColor(Color.FromColor(color));
        }
        public static Hsl FromColor(System.Windows.Media.Color color)
        {
            return FromColor(Color.FromColor(color));
        }

        public override string ToString()
        {
            return $"{Color.ColorCode} (Hue={Hue * 360:F0}, Saturation={Saturation * 255:F0}, Lightness={Lightness * 255:F0})";
        }
    }
}
