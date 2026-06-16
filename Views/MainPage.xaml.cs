namespace PokemonApp.Views;

using System.Collections.ObjectModel;
using PokemonApp.Models;
using PokemonApp.Services;
using PokemonApp.PokemonLoader;
using System.Diagnostics;
using PokemonApp.Views;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

// OPEN EMULATOR ~/Library/Android/sdk/emulator/emulator -avd MyAndroidVirtualDevice-API35

public partial class MainPage : ContentPage
{
    public MainPage(PokemonItem viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}