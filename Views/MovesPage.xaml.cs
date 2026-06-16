namespace PokemonApp.Views;

using PokemonApp.PokemonLoader;

public partial class MovesPage : ContentPage
{
    public MovesPage(PokemonItem viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}