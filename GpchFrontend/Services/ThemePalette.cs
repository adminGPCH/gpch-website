namespace GpchFrontend.Services
{
    public static class ThemePalette
    {
        public static string GetColor(string tema) => tema switch
        {
            "rosado" => "#f8bbd0",
            "burdeos" => "#642442",
            "rojo" => "#b71c1c",
            "amarillo" => "#fdd835",
            "verde" => "#00573a",
            "azul" => "#0e436e",
            _ => "#b71c1c"
        };

        public static string GetContraste(string tema) => tema switch
        {
            "rosado" => "#000000",
            "burdeos" => "#ffffff",
            "rojo" => "#ffffff",
            "amarillo" => "#000000",
            "verde" => "#ffffff",
            "azul" => "#ffffff",
            _ => "#ffffff"
        };

        public static int GetFontSizeBase(string tema) => tema switch
        {
            "rosado" => 16,
            "burdeos" => 15,
            "rojo" => 17,
            "amarillo" => 16,
            "verde" => 18,
            "azul" => 16,
            _ => 16
        };

        public static int GetSpacingBase(string tema) => tema switch
        {
            "rosado" => 8,
            "burdeos" => 6,
            "rojo" => 10,
            "amarillo" => 8,
            "verde" => 12,
            "azul" => 9,
            _ => 8
        };

        public static string GetBackgroundImage(string tema) => tema switch
        {
            "rosado" => "url('/images/fondos/fondo-rosado.webp')",
            "burdeos" => "url('/images/fondos/fondo-burdeos.webp')",
            "rojo" => "url('/images/fondos/fondo-rojo.webp')",
            "amarillo" => "url('/images/fondos/fondo-amarillo.webp')",
            "verde" => "url('/images/fondos/fondo-verde.webp')",
            "azul" => "url('/images/fondos/fondo-azul.webp')",
            _ => "Initial"
        };

    }
}
