namespace PokemonApp;

using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using PokemonApp.Models;
using PokemonApp.Services;
using PokemonApp.ViewModels;

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
            });

        builder.Services.AddHttpClient<PokemonApiService>();
        builder.Services.AddSingleton<PokemonViewModel>();
        builder.Services.AddSingleton<MainPage>();

        return builder.Build();
    }
}