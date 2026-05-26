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

    private string _searchText;
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
        if (string.IsNullOrWhiteSpace(SearchText))
            return;

        Result = await _searchService.SearchPokemonAsync(SearchText);
    }


    private void OnToggleModeExecuted()
    {
        var currentBackground = (Color)Application.Current.Resources["GlobalPageBackground"];
        if (currentBackground == Colors.White)
        {
            Application.Current.Resources["GlobalPageBackground"] = Colors.Black;
            Application.Current.Resources["GlobalTextColor"] = Colors.White;
        }
        else
        {
            Application.Current.Resources["GlobalPageBackground"] = Colors.White;
            Application.Current.Resources["GlobalTextColor"] = Colors.Black;
        }
    }
}