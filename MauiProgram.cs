using ChessAnalyzer.Services;
using ChessAnalyzer.ViewModel;
using ChessAnalyzer.Views;
using Microsoft.Extensions.Logging;

namespace ChessAnalyzer;

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
            });
        
        // Enregistrer le service API avec HttpClient
        builder.Services.AddSingleton<HttpClient>();
        builder.Services.AddSingleton<IChessService, ChessService>();

        // Enregistrer le ViewModel
        builder.Services.AddTransient<MainViewModel>();

        // Enregistrer la page principale
        builder.Services.AddTransient<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}