namespace PokemonApp.Views;

using PokemonApp.PokemonLoader;

public partial class AbilityPage : ContentPage
{
    public AbilityPage(PokemonItem viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
