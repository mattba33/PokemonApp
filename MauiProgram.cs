namespace PokemonApp;

using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using PokemonApp.Models;
using PokemonApp.Services;
using PokemonApp.PokemonLoader;
using PokemonApp.Views;

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
        builder.Services.AddSingleton<PokemonItem>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<SearchPage>();
        builder.Services.AddTransient<MovesPage>();
        builder.Services.AddTransient<AbilityPage>();

        return builder.Build();
    }
}