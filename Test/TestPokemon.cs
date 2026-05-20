// namespace PokemonApp.Test;

// using System.Diagnostics;
// using System.Net.Http;
// using PokemonApp.Models;
// using PokemonApp.Services;
// using PokemonApp.ViewModels;

// public class PokemonTest
// {
//     private readonly PokemonApiService _api;

//     public PokemonTest(PokemonApiService api)
//     {
//         _api = api;
//     }

//     public Task RunAsync()
//     {
//         return TestPokemon();
//     }

//     private async Task TestPokemon()
//     {
//         var vm = new PokemonViewModel(_api);

//         await vm.LoadPokemonAsync("10079");

//         Debug.WriteLine("=== POKEMON TEST ===");

//         Debug.WriteLine($"Name: {vm.Name}");
//         Debug.WriteLine($"Types: {string.Join(", ", vm.Types)}");
//         Debug.WriteLine($"Description: {vm.Description}");

//         Debug.WriteLine("=== STATS ===");

//         foreach (var stat in vm.Stats)
//         {
//             Debug.WriteLine($"{stat.Stat?.Name}: {stat.BaseStat}");
//         }

//         Debug.WriteLine("=== ABILITIES ===");

//         foreach (var ability in vm.Abilities)
//         {
//             Debug.WriteLine(ability);
//         }

//         Debug.WriteLine("=== MOVES ===");

//         foreach (var move in vm.Moves)
//         {
//             Debug.WriteLine(move);
//         }

//         Debug.WriteLine("=== EVOLUTION ===");

//         Debug.WriteLine(vm.EvolutionLine?.ToString() ?? "No evolution data");

//                 bool TEST = false; // Change this to true to run the test

//         if (TEST)
//         {
//             var client = new HttpClient();
//             var api = new PokemonApiService(client);
//             var test = new PokemonTest(api);

//             await test.RunAsync();
//         }
//     }
// }