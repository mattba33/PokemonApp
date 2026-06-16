namespace PokemonApp.ViewModels;

using PokemonApp.Models;
using PokemonApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using PokemonApp.Views;

[QueryProperty(nameof(PokemonName), "name")]
public class PokemonViewModel : INotifyPropertyChanged
{
    private readonly PokemonApiService _api;

    public PokemonViewModel(PokemonApiService api)
    {
        _api = api;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string name = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    
    private string _pokemonName = "";

    public string PokemonName
    {
        get => _pokemonName;
        set
        {
            _pokemonName = value;
            OnPropertyChanged();

            if (!string.IsNullOrWhiteSpace(value))
            {
                _ = LoadPokemonAsync(value);
            }
        }
    }

    public int PokedexNumber { get; set; }
    public string Name { get; set; } = "";
    public ObservableCollection<string> Types { get; set; } = new();
    public string TypesText => string.Join(", ", Types);
    public PokemonSprites? Sprites { get; set; }
    public string Description { get; set; } = "";
    public ObservableCollection<PokemonStat> Stats { get; set; } = new();  
    public string StatsText => string.Join(", ", Stats.Select(s => $"{s.Stat.Name}: {s.BaseStat}"));

    public ObservableCollection<AbilityItem> Abilities { get; set; } = new();

    private async Task LoadAbilitiesAsync(Pokemon pokemon)
    {
        Abilities.Clear();
        foreach (var ability in pokemon.Abilities)
        {
            var abilityinfo = await _api.GetAbilityDescriptionAsync(ability.Ability.Url);

            if (abilityinfo == null)
                continue;

            var text = _api.GetEnglishAbilityDescription(abilityinfo) ?? "";

            Abilities.Add(new AbilityItem
            {
                Name = ability.Ability.Name,
                Description = text,
                IsHidden = ability.IsHidden
            });
        }
        OnPropertyChanged(nameof(Abilities));
    }

    public ObservableCollection<MoveItem> Moves { get; set; } = new();

    private async Task LoadMovesAsync(Pokemon pokemon)
    {
        Moves.Clear();

        List<Task<MoveItem?>> moveTasks = new List<Task<MoveItem?>>();

        foreach (var move in pokemon.Moves)
        {
            Task<MoveItem?> task = LoadSingleMoveAsync(move);
            moveTasks.Add(task);
        }

        MoveItem?[] results = await Task.WhenAll(moveTasks);

        foreach (var result in results)
        {
            if (result != null)
            {
                Moves.Add(result);
            }
        }

        OnPropertyChanged(nameof(Moves));
    }

    private async Task<MoveItem?> LoadSingleMoveAsync(PokemonMoveSlot move)
    {
        var moveInfo = await _api.GetMoveInfoAsync(move.Move.Url);

        if (moveInfo == null)
            return null;

        MoveItem viewModel = new MoveItem();

        viewModel.Name = move.Move.Name;

        viewModel.Power = moveInfo.Power;
        viewModel.Accuracy = moveInfo.Accuracy;

        viewModel.Type = moveInfo.Type?.Name ?? "";

        viewModel.Description = _api.GetEnglishMoveDescription(moveInfo) ?? "";

        return viewModel;
    }

    public EvolutionLine? EvolutionLine { get; set; }

    public async Task LoadPokemonAsync(string pokemonName)
    {
        var pokemon = await _api.GetPokemonAsync(pokemonName);
        if (pokemon == null) return;

        PokedexNumber = pokemon.PokedexNumber;
        Name = pokemon.Name ?? "";

        Types.Clear();
        foreach (var type in pokemon.Types)
        {
            Types.Add(type.Type.Name);
        }
        
        Sprites = pokemon.Sprites.FrontDefault != null ? pokemon.Sprites : null;

        var species = await _api.GetDescriptionAsync(pokemon.Species.Url);
        if (species != null)
        {
            Description = _api.GetLatestEnglishPokemonDescription(species) ?? "";
        }

        Stats.Clear();
        foreach (var stat in pokemon.Stats)
        {
            Stats.Add(stat);
        }

        await LoadMovesAsync(pokemon);
        await LoadAbilitiesAsync(pokemon);

        if (species?.EvolutionChain?.Url != null)
        {
            EvolutionLine = await _api.GetEvolutionLineAsync(species.EvolutionChain.Url);
        }

        OnPropertyChanged(nameof(PokedexNumber));
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(TypesText));
        OnPropertyChanged(nameof(Sprites));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(StatsText));
        OnPropertyChanged(nameof(EvolutionLine));
    } 
}