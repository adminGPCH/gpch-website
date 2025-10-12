namespace GpchFrontend.Services
{
    public static class ThemePalette
    {
        public static string GetColor(string tema) => tema switch
        {
            "emocion" => "#f8bbd0",
            "acompanamiento" => "#642442",
            "vital" => "#b71c1c",
            "revision" => "#fdd835",
            "legado" => "#00573a",
            "azul" => "#0e436e",
            _ => "#b71c1c"
        };

        public static string GetContraste(string tema) => tema switch
        {
            "emocion" => "#000000",
            "acompanamiento" => "#ffffff",
            "vital" => "#ffffff",
            "revision" => "#000000",
            "legado" => "#ffffff",
            "azul" => "#ffffff",
            _ => "#ffffff"
        };

        public static int GetFontSizeBase(string tema) => tema switch
        {
            "emocion" => 16,
            "acompanamiento" => 15,
            "vital" => 17,
            "revision" => 16,
            "legado" => 18,
            "azul" => 16,
            _ => 16
        };

        public static int GetSpacingBase(string tema) => tema switch
        {
            "emocion" => 8,
            "acompanamiento" => 6,
            "vital" => 10,
            "revision" => 8,
            "legado" => 12,
            "azul" => 9,
            _ => 8
        };

        public static string GetBackgroundTexture(string tema) => tema switch
        {
            "emocion" => "url('/images/bg-emocion.png')",
            "acompanamiento" => "url('/images/bg-acompanamiento.png')",
            "vital" => "url('/images/bg-vital.png')",
            "revision" => "url('/images/bg-revision.png')",
            "legado" => "url('/images/bg-legado.png')",
            "azul" => "url('/images/bg-azul.png')",
            _ => "none"
        };
    }
}
