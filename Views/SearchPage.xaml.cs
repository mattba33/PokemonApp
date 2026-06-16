namespace PokemonApp.Views;

using PokemonApp.Models;
using PokemonApp.Services;
using Microsoft.Maui.Storage;

public partial class SearchPage : ContentPage
{
    private readonly PokemonApiService _api;
    private const string ThemeKey = "AppTheme";

    public SearchPage(PokemonApiService api)
    {
        InitializeComponent();
        _api = api;
    }

    private async void SearchButton_Clicked(object sender, EventArgs e)
    {
        string searchText = SearchEntry.Text?.ToLower() ?? "";

        Pokemon? result = await _api.GetPokemonAsync(searchText);

        if (result == null)
        {
            StatusLabel.Text = "Pokemon not found";
            return;
        }

        StatusLabel.Text = string.Empty;

        await Shell.Current.GoToAsync($"//MainPage?name={result.Name}");
    }

    private void ToggleTheme_Clicked(object sender, EventArgs e)
    {
        var currentBackground =
            (Color)Application.Current.Resources["GlobalPageBackground"];

        bool isLightMode = currentBackground == Colors.White;

        if (isLightMode)
        {
            Application.Current.Resources["GlobalPageBackground"] = Colors.Black;
            Application.Current.Resources["GlobalTextColor"] = Colors.White;

            Preferences.Set(ThemeKey, "Dark");
        }
        else
        {
            Application.Current.Resources["GlobalPageBackground"] = Colors.White;
            Application.Current.Resources["GlobalTextColor"] = Colors.Black;

            Preferences.Set(ThemeKey, "Light");
        }
    }
}