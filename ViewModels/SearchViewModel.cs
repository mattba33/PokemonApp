namespace PokemonApp.ViewModels;

using PokemonApp.Models;
using PokemonApp.Services;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

public class SearchViewModel : BindableObject
{
    private readonly SearchService _searchService;
    private const string ThemeKey = "AppTheme";

    public SearchViewModel(SearchService searchService)
    {
        _searchService = searchService;

        SearchCommand = new Command(OnSearchCommandExecuted);
        ToggleMode = new Command(OnToggleModeExecuted);
    }

    private string _searchText = string.Empty;
    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
        }
    }

    private Pokemon? _result;
    public Pokemon? Result
    {
        get => _result;
        set
        {
            _result = value;
            OnPropertyChanged();
        }
    }

    private string _statusMessage = string.Empty;
    public string? StatusMessage
    {
        get => _statusMessage;
        set
        {
            _statusMessage = value;
            OnPropertyChanged();
        }
    }

    public ICommand SearchCommand { get; }
    public ICommand ToggleMode { get; }

    private async void OnSearchCommandExecuted()
    {
        await SearchAsync();

        if (Result != null)
        {
            await Shell.Current.GoToAsync($"//MainPage?name={Result.Name}");
        }
    }

    private async Task SearchAsync()
    {
        Result = await _searchService.SearchPokemonAsync(SearchText);

        if (Result == null)
        {
            StatusMessage = "Pokemon Not found";
            
        }
        else
        {
            StatusMessage = string.Empty;
        }
    }

    private void OnToggleModeExecuted()
    {
        var currentBackground = (Color)Application.Current.Resources["GlobalPageBackground"];

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