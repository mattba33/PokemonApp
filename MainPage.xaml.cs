namespace PokemonApp;

using System.Collections.ObjectModel;
using PokemonApp.Models;
using PokemonApp.Services;
using PokemonApp.ViewModels;
using System.Diagnostics;
using Test;


// OPEN EMULATOR ~/Library/Android/sdk/emulator/emulator -avd MyAndroidVirtualDevice-API35

public partial class MainPage : ContentPage
{
    private readonly PokemonViewModel _viewModel;
    public MainPage(PokemonViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadPokemonAsync(10079);

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