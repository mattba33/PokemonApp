namespace PokemonApp.ViewModels;

using PokemonApp.Models;
using PokemonApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Microsoft.Maui.Controls;

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

    public string Name { get; set; } = "";
    public ObservableCollection<string> Types { get; set; } = new();
    public string TypesText => string.Join(", ", Types);
    public PokemonSprites? Sprites { get; set; }
    public string Description { get; set; } = "";
    public ObservableCollection<PokemonStat> Stats { get; set; } = new();  
    public string StatsText => string.Join(", ", Stats.Select(s => $"{s.Stat.Name}: {s.BaseStat}"));

    public ObservableCollection<string> Abilities { get; set; } = new();
    public string AbilitiesText => string.Join(", ", Abilities);
    public ObservableCollection<MoveViewModel> Moves { get; set; } = new();

    private async Task LoadMovesAsync(Pokemon pokemon)
    {
        Moves.Clear();
        foreach (var move in pokemon.Moves.Take(10))
        {
            var moveinfo = await _api.GetMoveInfoAsync(move.Move.Url);

            if (moveinfo == null)
                continue;

            Moves.Add(new MoveViewModel
            {
                Name = move.Move.Name,
                Power = moveinfo.Power,
                Accuracy = moveinfo.Accuracy,
                Type = moveinfo.Type.Name,
                Description = _api.GetEnglishMoveDescription(moveinfo) ?? ""
            });
        }
        OnPropertyChanged(nameof(Moves));
    }

    public EvolutionLine? EvolutionLine { get; set; }

    public async Task LoadPokemonAsync(string pokemonName)
    {
        var pokemon = await _api.GetPokemonAsync(pokemonName);
        if (pokemon == null) return;

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

        Abilities.Clear();
        foreach (var ability in pokemon.Abilities)
        {
            var abilityinfo = await _api.GetAbilityDescriptionAsync(ability.Ability.Url);

            if (abilityinfo != null)
            {
                var text = _api.GetEnglishAbilityDescription(abilityinfo);
                var isHidden = ability.IsHidden ? " (Hidden)" : "";

                Abilities.Add($"{ability.Ability.Name}{isHidden} - {text}");
            }
        }

        if (species?.EvolutionChain?.Url != null)
        {
            EvolutionLine = await _api.GetEvolutionLineAsync(species.EvolutionChain.Url);
        }

        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(TypesText));
        OnPropertyChanged(nameof(Sprites));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(StatsText));
        OnPropertyChanged(nameof(AbilitiesText));
        OnPropertyChanged(nameof(EvolutionLine));
    } 
}