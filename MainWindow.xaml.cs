using DataObjects;
using LogicLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokeApiNet;
using System.Security.Cryptography;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace PokeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string baseDirectory = AppContext.BaseDirectory;
        private PokemonManager _pokemonManager = new PokemonManager();
        private DataObjects.Pokemon CurrentPokemon;
        PokeApiClient pokeClient = new PokeApiClient();
        List<DataObjects.Pokemon> pokemonList = new List<DataObjects.Pokemon>();


        List<String> KantoLocations = new List<string>();
        List<String> JhotoLocations = new List<string>();
        List<String> HoennLocations = new List<string>();
        List<String> SinnohLocations = new List<string>();
        List<String> UnovaLocations = new List<string>();
        List<String> KalosLocations = new List<string>();
        List<String> AlolaLocations = new List<string>();
        List<String> GalarLocations = new List<string>();
        List<String> PaldeaLocations = new List<string>();

        string pokemonLocation;

        public MainWindow()
        {
            InitializeComponent();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int generation = 1;
            GetKantoLocations(generation);
            PokemonItems();
        }
        public async void GetKantoLocations(int generation)
        {
            PokeApiNet.Region region = await pokeClient.GetResourceAsync<PokeApiNet.Region>(generation);
            foreach (var location in region.Locations)
            {
                PokeApiNet.Location locationDetails = await pokeClient.GetResourceAsync<PokeApiNet.Location>(location.Name);
                KantoLocations.Add(locationDetails.Name);
            }
        }



        public async void GetPokemonLocation(PokeApiNet.Pokemon pokemon, int generation)
        {
            foreach (var kanto in KantoLocations)
            {
                locationAreaEncounter = await pokeClient.GetResourceAsync<LocationArea>(kanto);
                foreach (var encounter in locationAreaEncounter.PokemonEncounters)
                {
                    if (encounter.Pokemon.Name == pokemon.Name)
                    {
                        MessageBox.Show(pokemon.Name);
                        pokemonLocation = kanto;
                    }
                }
            }
        }


        private async void PokemonItems()
        {
            for (int i = 1; i <= 3; i++)
            {
                int dexNumber = i;
                int generation = 1;
                PokeApiNet.Pokemon pokemon = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(dexNumber);
                GetPokemonLocation(pokemon, generation);
                var newPokemon = new DataObjects.Pokemon()
                {
                    PokemonID = 100000,
                    Name = pokemon.Name.ToUpper(),
                    DexNumber = dexNumber,
                    Sprite = pokemon.Sprites.FrontDefault,
                    CaughtOrNot = false,
                    Location = pokemonLocation
                };
                pokemonList.Add(newPokemon);
            }
            icPokemon.ItemsSource = pokemonList;
        }

        private void mnuGen1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}






            //List<Move> allMoves = await pokeClient.GetResourceAsync(pokemon.Moves.Select(move => move.Move));
            //List<PokeApiNet.Type> pokemonTypes = await pokeClient.GetResourceAsync(pokemon.Types.Select(type => type.Type));

            //var w = pokemon.LocationAreaEncounters;
            //List<LocationAreaEncounter> locations = await pokeClient.GetResourceAsync(pokemon.LocationAreaEncounters.ToList());

            //string locationAreaEncountersUrl = pokemon.LocationAreaEncounters;

            //Make an asynchronous request to get the LocationAreaEncounter resource
            // List<VersionGameIndex> games = await pokeClient.GetResourceAsync(pokemon.GameIndicies.Select(version => version.Version));
            //var a = pokemon.GameIndicies[1];
            //MessageBox.Show(a.ToString());

            //Deserialize the JSON response into a list of LocationAreaEncounter objects
            // List<LocationAreaEncounter> locations = JsonConvert.DeserializeObject<List<LocationAreaEncounter>>(apiResponse);

            //MessageBox.Show(pokemonTypes[0].Name.ToString());
            //var testings = pokemon.LocationAreaEncounters;

            //MessageBox.Show(allMoves.Count().ToString());


            //Berry cheri = await pokeClient.GetResourceAsync<Berry>(4);
            //MessageBox.Show(cheri.Name);

            //EncounterMethod encounter = await pokeClient.GetResourceAsync<EncounterMethod>(1);
            //MessageBox.Show(encounter.Name);


            //var test = pokemon.LocationAreaEncounters;
            //MessageBox.Show(test);

            //Location location = await pokeClient.GetResourceAsync<Location>(LocationArea);
            //MessageBox.Show(location.Region.Name.ToString());
            //MessageBox.Show(location.Name);

            //Encounter game = await pokeClient.GetResourceAsync<Encounter>();
            //MessageBox.Show(location.Region.Name.ToString());
            //MessageBox.Show(location.Name);



            //int pokedexNumber = 1;
            //PokeApiNet.Pokemon pokemon = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(pokedexNumber);

            //PokeApiNet.Generation gen = await pokeClient.GetResourceAsync<PokeApiNet.Generation>(2);
            //int startingPosition = 152;
            //int test = (gen.PokemonSpecies.Count() + startingPosition);
            //int PokemonIndex = 0;

            //for (int i = startingPosition; i < (gen.PokemonSpecies.Count() + startingPosition); i++)
            //{
            //    PokeApiNet.Pokemon _pokemon = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(i);
            //    PokemonIndex++;
            //    var newPokemon = new DataObjects.Pokemon()
            //    {
            //        PokemonID = 100000,
            //        Name = _pokemon.Name.ToUpper(),
            //        DexNumber = _pokemon.Order,
            //        Sprite = _pokemon.Sprites.FrontDefault,
            //        CaughtOrNot = false
            //    };
            //    pokemonList.Add(newPokemon);
            //}



            //PokeApiNet.LocationArea locationAreaEncounter = await pokeClient.GetResourceAsync<LocationArea>(1);
            //foreach (var encounter in locationAreaEncounter.PokemonEncounters)
            //{
            //    if (encounter.Pokemon.Name == "tentacruel")
            //    {
            //        MessageBox.Show(locationAreaEncounter.Name);
            //    }
            //}



            //for (int i = 1; i <= 9; i++)
            //{
            //    PokeApiNet.Region region = await pokeClient.GetResourceAsync<PokeApiNet.Region>(i);
            //    // Iterate through the locations in the region
            //    switch (i)
            //    {
            //        case 1:
            //            foreach (var location in region.Locations)
            //            {
            //                // Get information about the location
            //                PokeApiNet.Location locationDetails = await pokeClient.GetResourceAsync<PokeApiNet.Location>(location.Name);
            //                KantoLocations.Add(locationDetails.Name);
            //            }
            //            break;
            //        case 2:
            //            foreach (var location in region.Locations)
            //            {
            //                // Get information about the location
            //                PokeApiNet.Location locationDetails = await pokeClient.GetResourceAsync<PokeApiNet.Location>(location.Name);
            //                JhotoLocations.Add(locationDetails.Name);
            //            }
            //            break;
            //        case 3:
            //            foreach (var location in region.Locations)
            //            {
            //                // Get information about the location
            //                PokeApiNet.Location locationDetails = await pokeClient.GetResourceAsync<PokeApiNet.Location>(location.Name);
            //                HoennLocations.Add(locationDetails.Name);
            //            }
            //            break;
            //        case 4:
            //            foreach (var location in region.Locations)
            //            {
            //                // Get information about the location
            //                PokeApiNet.Location locationDetails = await pokeClient.GetResourceAsync<PokeApiNet.Location>(location.Name);
            //                SinnohLocations.Add(locationDetails.Name);
            //            }
            //            break;
            //        case 5:
            //            foreach (var location in region.Locations)
            //            {
            //                // Get information about the location
            //                PokeApiNet.Location locationDetails = await pokeClient.GetResourceAsync<PokeApiNet.Location>(location.Name);
            //                UnovaLocations.Add(locationDetails.Name);
            //            }
            //            break;
            //        case 6:
            //            foreach (var location in region.Locations)
            //            {
            //                // Get information about the location
            //                PokeApiNet.Location locationDetails = await pokeClient.GetResourceAsync<PokeApiNet.Location>(location.Name);
            //                KalosLocations.Add(locationDetails.Name);
            //            }
            //            break;
            //        case 7:
            //            foreach (var location in region.Locations)
            //            {
            //                // Get information about the location
            //                PokeApiNet.Location locationDetails = await pokeClient.GetResourceAsync<PokeApiNet.Location>(location.Name);
            //                AlolaLocations.Add(locationDetails.Name);
            //            }
            //            break;
            //        case 8:
            //            foreach (var location in region.Locations)
            //            {
            //                // Get information about the location
            //                PokeApiNet.Location locationDetails = await pokeClient.GetResourceAsync<PokeApiNet.Location>(location.Name);
            //                GalarLocations.Add(locationDetails.Name);
            //            }
            //            break;
            //        case 9:
            //            foreach (var location in region.Locations)
            //            {
            //                // Get information about the location
            //                PokeApiNet.Location locationDetails = await pokeClient.GetResourceAsync<PokeApiNet.Location>(location.Name);
            //                PaldeaLocations.Add(locationDetails.Name);
            //            }
            //            break;
            //        default:
            //            break;
            //    }
            //}


            //List<Move> allMoves = await pokeClient.GetResourceAsync(pokemon.Moves.Select(move => move.Move));
            //MessageBox.Show(allMoves[0].Name);



            //LocationArea locationArea = await pokeClient.GetResourceAsync<LocationArea>(1);
            //MessageBox.Show(locationArea.Name.ToString());


            //List<PokemonEncounter> pokemonEncounter = locationEncounters.PokemonEncounters.ToList();
            //MessageBox.Show(pokemonEncounter[1].ToString());

