namespace PokemonApp;

using System.Collections.ObjectModel;
using PokemonApp.Models;
using PokemonApp.Services;
using PokemonApp.ViewModels;
using System.Diagnostics;
using PokemonApp.Views;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

// OPEN EMULATOR ~/Library/Android/sdk/emulator/emulator -avd MyAndroidVirtualDevice-API35

public partial class MainPage : ContentPage
{
    private readonly PokemonViewModel viewModel;
    public MainPage(PokemonViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnReturnToSearchButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//SearchPage");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}