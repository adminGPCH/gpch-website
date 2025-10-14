namespace GpchFrontend.Utils
{
    public static class ColorUtils
    {
        public static (int R, int G, int B) HexToRgb(string hex)
        {
            hex = hex.Replace("#", "");
            if (hex.Length == 3)
                hex = string.Concat(hex.Select(c => $"{c}{c}"));

            int r = Convert.ToInt32(hex[0..2], 16);
            int g = Convert.ToInt32(hex[2..4], 16);
            int b = Convert.ToInt32(hex[4..6], 16);
            return (r, g, b);
        }

        public static double GetLuminance(int r, int g, int b)
        {
            double RsRGB = r / 255.0;
            double GsRGB = g / 255.0;
            double BsRGB = b / 255.0;

            double R = RsRGB <= 0.03928 ? RsRGB / 12.92 : Math.Pow((RsRGB + 0.055) / 1.055, 2.4);
            double G = GsRGB <= 0.03928 ? GsRGB / 12.92 : Math.Pow((GsRGB + 0.055) / 1.055, 2.4);
            double B = BsRGB <= 0.03928 ? BsRGB / 12.92 : Math.Pow((BsRGB + 0.055) / 1.055, 2.4);

            return 0.2126 * R + 0.7152 * G + 0.0722 * B;
        }

        public static string GetContrastColor(string hexColor)
        {
            var (r, g, b) = HexToRgb(hexColor);
            double luminance = GetLuminance(r, g, b);
            return luminance > 0.5 ? "#000000" : "#FFFFFF";
        }

        public static string GetTextShadow(string hexColor)
        {
            string contrast = GetContrastColor(hexColor);
            return $"1px 1px 2px {contrast}";
        }
    }
}
