namespace PokemonApp.Views;

using PokemonApp.ViewModels;

public partial class AbilityPage : ContentPage
{
    public AbilityPage(PokemonViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
