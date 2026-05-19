namespace PokemonApp.Views;

using PokemonApp.ViewModels;

public partial class SearchPage : ContentPage
{
    public SearchPage(SearchViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

}