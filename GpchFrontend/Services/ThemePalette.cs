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

        public static string GetBackgroundTexture(string tema)
        {
            var color = GetColor(tema).Replace("#", "%23");

            var svg = $@"
                <svg xmlns='http://www.w3.org/2000/svg' width='6' height='6'>
                  <line x1='0' y1='0' x2='6' y2='0' stroke='{color}' stroke-width='0.4' opacity='0.08'/>
                  <line x1='0' y1='1' x2='6' y2='1' stroke='{color}' stroke-width='0.4' opacity='0.04'/>
                  <line x1='0' y1='2' x2='6' y2='2' stroke='{color}' stroke-width='0.4' opacity='0.02'/>
                  <line x1='0' y1='3' x2='6' y2='3' stroke='{color}' stroke-width='0.4' opacity='0.04'/>
                  <line x1='0' y1='4' x2='6' y2='4' stroke='{color}' stroke-width='0.4' opacity='0.08'/>
                  <line x1='0' y1='5' x2='6' y2='5' stroke='{color}' stroke-width='0.4' opacity='0.04'/>
                  <line x1='0' y1='6' x2='6' y2='6' stroke='{color}' stroke-width='0.4' opacity='0.02'/>
                  <line x1='0' y1='0' x2='0' y2='6' stroke='{color}' stroke-width='0.4' opacity='0.08'/>
                  <line x1='1' y1='0' x2='1' y2='6' stroke='{color}' stroke-width='0.4' opacity='0.04'/>
                  <line x1='2' y1='0' x2='2' y2='6' stroke='{color}' stroke-width='0.4' opacity='0.02'/>
                  <line x1='3' y1='0' x2='3' y2='6' stroke='{color}' stroke-width='0.4' opacity='0.04'/>
                  <line x1='4' y1='0' x2='4' y2='6' stroke='{color}' stroke-width='0.4' opacity='0.08'/>
                  <line x1='5' y1='0' x2='5' y2='6' stroke='{color}' stroke-width='0.4' opacity='0.04'/>
                  <line x1='6' y1='0' x2='6' y2='6' stroke='{color}' stroke-width='0.4' opacity='0.02'/>
                </svg>";
            var encoded = Uri.EscapeDataString(svg);
            return $"url(\"data:image/svg+xml,{encoded}\")";
        }



    }
}
