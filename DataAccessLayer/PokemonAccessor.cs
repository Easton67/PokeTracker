using DataAccessInterfaces;
using DataObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PokemonAccessor : IPokemonAccessor
    {
        public async Task<Pokemon> GetPokemonInformationAsync(int DexNumber)
        {
            Pokemon pokemon = GetPokemonName(DexNumber).Result;
            return pokemon;
        }

        public async Task<Pokemon> GetPokemonName(int DexNumber)
        {
            Pokemon pokemonInfo = new Pokemon();

            // PokeAPI base URL
            string baseUrl = "https://pokeapi.co/api/v2/";

            string pokemonEndpoint = $"pokemon/{DexNumber}/";

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"{baseUrl}{pokemonEndpoint}";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        pokemonInfo = JsonConvert.DeserializeObject<Pokemon>(responseBody);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
            return pokemonInfo;
        }
    }
}
