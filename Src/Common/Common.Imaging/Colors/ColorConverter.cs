namespace Auxilia.Imaging
{
    public static class ColorConverter
    {
        public static Color FromDrawingColor(this System.Drawing.Color color)
        {
            return new Color(color.A, color.R, color.G, color.B);
        }
        public static System.Drawing.Color ToDrawingColor(this Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static Color FromMediaColor(this System.Windows.Media.Color color)
        {
            return new Color(color.A, color.R, color.G, color.B);
        }
        public static System.Windows.Media.Color ToMediaColor(this Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}