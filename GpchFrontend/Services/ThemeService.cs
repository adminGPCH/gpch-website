namespace GpchFrontend.Services
{
    public class ThemeService
    {
        public string TemaActual { get; private set; } = "azul";
        public string FondoActual { get; set; } = "url('/images/fondos/fondo-azul1.webp')";

        public event Action? OnThemeChanged;
        public bool CarruselActivo { get; set; } = true;

        public void CambiarTema(string nuevoTema)
        {
            TemaActual = nuevoTema;
            FondoActual = ThemePalette.GetBackgroundImage(nuevoTema);
            OnThemeChanged?.Invoke();
        }
        public void CambiarFondo(string nuevaImagen)
        {
            FondoActual = nuevaImagen;
            OnThemeChanged?.Invoke();
        }

        public void CambiarTemaConFondo(string nuevoTema, string nuevaImagen)
        {
            TemaActual = nuevoTema;
            FondoActual = nuevaImagen;
            OnThemeChanged?.Invoke();
        }

    }
}