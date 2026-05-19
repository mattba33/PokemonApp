namespace PokemonApp.ViewModels;

using PokemonApp.Models;
using PokemonApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

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

    public string Name { get; set; } = "";
    public ObservableCollection<string> Types { get; set; } = new();
    public string TypesText => string.Join(", ", Types);
    public PokemonSprites? Sprites { get; set; }
    public string Description { get; set; } = "";
    public ObservableCollection<PokemonStat> Stats { get; set; } = new();  
    public string StatsText => string.Join(", ", Stats.Select(s => $"{s.Stat.Name}: {s.BaseStat}"));

    public ObservableCollection<string> Abilities { get; set; } = new();
    public string AbilitiesText => string.Join(", ", Abilities);
    public ObservableCollection<string> Moves { get; set; } = new();
    public string MovesText => string.Join("\n", Moves);

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

        Moves.Clear();
        foreach (var move in pokemon.Moves.Take(1))
        {
            var moveinfo = await _api.GetMoveInfoAsync(move.Move.Url);

            if (moveinfo != null)
            {
                var text = _api.GetEnglishMoveDescription(moveinfo);
                Moves.Add($"{move.Move.Name} - Power: {moveinfo.Power} Accuracy: {moveinfo.Accuracy} Type: {moveinfo.Type.Name} - {text}");
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
        OnPropertyChanged(nameof(MovesText));
        OnPropertyChanged(nameof(EvolutionLine));
    }
}