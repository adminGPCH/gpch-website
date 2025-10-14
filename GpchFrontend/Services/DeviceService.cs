using Microsoft.JSInterop;

namespace GpchFrontend.Services;

public class DeviceService(IJSRuntime js)
{
    public async Task<int> GetDeviceWidthAsync()
    {
        return await js.InvokeAsync<int>("getDeviceWidth");
    }

    public static string GetDeviceType(int width)
    {
        if (width < 600) return "mobile";
        if (width < 960) return "tablet";
        return "desktop";
    }

    public static double GetFluidSize(double min, double max, int width)
    {
        int minWidth = 320;
        int maxWidth = 1920;
        var clamped = Math.Min(Math.Max(width, minWidth), maxWidth);
        var scale = (clamped - minWidth) / (double)(maxWidth - minWidth);
        return min + (max - min) * scale;
    }
}
