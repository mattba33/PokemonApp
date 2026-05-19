namespace PokemonApp;

using System.Collections.ObjectModel;
using PokemonApp.Models;
using PokemonApp.Services;
using PokemonApp.ViewModels;
using System.Diagnostics;
using Test;
using PokemonApp.Views;
using System.Threading.Tasks;


// OPEN EMULATOR ~/Library/Android/sdk/emulator/emulator -avd MyAndroidVirtualDevice-API35

public partial class MainPage : ContentPage
{
    bool openingPage = false; 
    private readonly PokemonViewModel _viewModel;
    public MainPage(PokemonViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private async void OnReturnToSearchButtonClicked(object sender, EventArgs e)
    {
        if (openingPage) { return; }
        openingPage = true;

        await Shell.Current.GoToAsync("//SearchPage");

        openingPage = false;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadPokemonAsync("rayquaza-mega");

        bool TEST = false; // Change this to true to run the test

        if (TEST)
        {
            var client = new HttpClient();
            var api = new PokemonApiService(client);
            var test = new PokemonTest(api);

            await test.RunAsync();
        }
    }
}