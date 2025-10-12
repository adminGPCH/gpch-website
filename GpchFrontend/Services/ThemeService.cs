namespace GpchFrontend.Services
{
    public class ThemeService
    {
        public string TemaActual { get; private set; } = "vital";

        public event Action? OnThemeChanged;

        public void CambiarTema(string nuevoTema)
        {
            if (TemaActual != nuevoTema)
            {
                TemaActual = nuevoTema;
                OnThemeChanged?.Invoke();
            }
        }
    }
}
