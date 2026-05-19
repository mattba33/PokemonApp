namespace PokemonApp.ViewModels;

using PokemonApp.Models;
using PokemonApp.Services;
using System.Windows.Input;
public class SearchViewModel : BindableObject
{
    private readonly SearchService _searchService;

    public SearchViewModel(SearchService searchService)
    {
        _searchService = searchService;
        SearchCommand = new Command(async () => await SearchAsync());
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

    private Pokemon _result;
    public Pokemon Result
    {
        get => _result;
        set
        {
            _result = value;
            OnPropertyChanged();
        }
    }

    public ICommand SearchCommand { get; }

    private async Task SearchAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
            return;

        Result = await _searchService.SearchPokemonAsync(SearchText);
    }

}