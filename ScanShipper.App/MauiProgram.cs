using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;

namespace ScanShipper.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa_solid.ttf", "FontAwesome");
                })
                .UseBarcodeReader()
                ;

#if DEBUG
    		builder.Logging.AddDebug();
#endif


            return builder.Build();
        }
    }
}
