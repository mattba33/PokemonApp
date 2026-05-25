namespace PokemonApp.Views;

using PokemonApp.ViewModels;

public partial class MovesPage : ContentPage
{
    public MovesPage(PokemonViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}