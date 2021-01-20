using System.Linq;

namespace Auxilia.Imaging
{
    public struct Hsb
    {
        private Color _color;

        private float _hue;
        private float _saturation;
        private float _brightness;

        public Hsb(float hue, float saturation, float brightness)
        {
            _color = Color.FromHsb(hue, saturation, brightness);

            _hue = hue.Clamp(0, 1);
            _saturation = saturation.Clamp(0, 1);
            _brightness = brightness.Clamp(0, 1);
        }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;

                Hsb hsb = FromColor(value);
                _hue = hsb._hue;
                _saturation = hsb._saturation;
                _brightness = hsb._brightness;
            }
        }

        public float Hue
        {
            get => _hue;
            set => Color = Color.FromHsb(value, _saturation, _brightness);
        }
        public float Saturation
        {
            get => _saturation;
            set => Color = Color.FromHsb(_hue, value, _brightness);
        }
        public float Brightness
        {
            get => _brightness;
            set => Color = Color.FromHsb(_hue, _saturation, value);
        }

        public static Hsb FromColor(Color color)
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

                hue = (hue / 6 + 1) % 1;
            }

            float saturation = maxValue.Equals(0)
                ? 0
                : chroma / maxValue;

            float brightness = maxValue;

            return new Hsb(hue, saturation, brightness);
        }
        public static Hsb FromColor(System.Drawing.Color color)
        {
            return FromColor(Color.FromColor(color));
        }
        public static Hsb FromColor(System.Windows.Media.Color color)
        {
            return FromColor(Color.FromColor(color));
        }

        public override string ToString()
        {
            return $"{Color.ColorCode} (Hue={Hue * 360:F0}, Saturation={Saturation * 255:F0}, Brightness={Brightness * 255:F0})";
        }
    }
}
