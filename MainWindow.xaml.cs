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
using System.Text.RegularExpressions;
using System.Security.Policy;
using System.Globalization;
using System.Diagnostics;
using Microsoft.Extensions.Primitives;

namespace PokeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    class Area
    {
        public string Identifier { get; set; }
        public string Location { get;set; }
        public string ID { get; set; }
    }

    public partial class MainWindow : Window
    {
        private string baseDirectory = AppContext.BaseDirectory;
        private PokemonManager _pokemonManager = new PokemonManager();
        private DataObjects.Pokemon CurrentPokemon;
        PokeApiClient pokeClient = new PokeApiClient();
        List<DataObjects.Pokemon> pokemonList = new List<DataObjects.Pokemon>();

        List<String> KantoLocations = new List<string>();
        List<String> KantoID = new List<string>();

        List<String> JhotoLocations = new List<string>();
        List<String> JhotoID = new List<string>();

        List<String> HoennLocations = new List<string>();
        List<String> HoennID = new List<string>();

        List<String> SinnohLocations = new List<string>();
        List<String> SinnohID = new List<string>();

        List<String> UnovaLocations = new List<string>();
        List<String> UnovaID = new List<string>();

        List<String> KalosLocations = new List<string>();
        List<String> KalosID = new List<string>();

        List<String> AlolaLocations = new List<string>();
        List<String> AlolaID = new List<string>();

        List<String> GalarLocations = new List<string>();
        List<String> GalarID = new List<string>();

        List<String> PaldeaLocations = new List<string>();
        List<String> PaldeaID = new List<string>();

        List<Area> areas;


        string pokemonLocation;
        string pokeLocation = "Location Not Found";

        public MainWindow()
        {
            InitializeComponent();

            areas = new List<Area>();

            string[] names = new string[]
            {
                "Kanto",
                "Jhoto",
                "Hoenn",
                "Sinnoh",
                "Unova",
                "Kalos",
                "Alola",
                "Galar",
                "Paldea"
            };
            foreach(string name in names)
            {
                areas.Add(new Area()
                {
                    Identifier = name,
                    Location = "",
                    ID = ""
                });
            }
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int dexNumber = 585;
            PokeApiNet.Pokemon pokemon = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(dexNumber);
            string gameName = "Black";
            PokemonItems(dexNumber, pokemon, gameName);
        }

        public async void PokemonItems(int dexNumber, PokeApiNet.Pokemon pokemon, string gameName)
        {
            string pokeLocation = GetLocation(dexNumber, gameName).Replace("-", " ").ToUpper();

            var newPokemon = new DataObjects.Pokemon()
            {
                PokemonID = 100000,
                Name = pokemon.Name.ToUpper(),
                DexNumber = dexNumber,
                Sprite = pokemon.Sprites.FrontDefault,
                CaughtOrNot = false,
                Location = pokeLocation,
            };
            pokemonList.Add(newPokemon);
            icPokemon.ItemsSource = pokemonList;
        }

        private string GetLocation(int pokemonNumber, string gameName)
        {
            // Specify the path to the Python interpreter (python.exe)
            string pythonPath = @"C:\Users\67Eas\AppData\Local\Microsoft\WindowsApps\python.exe";

            // Specify the path to your Python script
            string scriptPath = @"C:\Users\67Eas\Downloads\pokeAPI\main.py";

            // Create a command string with the script path and arguments
            string command = $"{scriptPath} {pokemonNumber} \"{gameName}\"";

            // Create a new process start info
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = command,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            // Create a new process and start it
            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();

                // Read the output (if needed)
                string output = process.StandardOutput.ReadToEnd();

                // Wait for the process to exit
                process.WaitForExit();

                return output;
            }
        }



        //private async void PokemonItems()
        //{
        //        for (int i = 1; i <= 10; i++)    
        //        {
        //            int dexNumber = i;
        //    int generation = 1;
        //    PokeApiNet.Pokemon pokemon = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(dexNumber);
        //    GetPokemonLocation(pokemon, generation);
        //    var newPokemon = new DataObjects.Pokemon()
        //    {
        //        PokemonID = 100000,
        //        Name = pokemon.Name.ToUpper(),
        //        DexNumber = dexNumber,
        //        Sprite = pokemon.Sprites.FrontDefault,
        //        CaughtOrNot = false,
        //        Location = pokeLocation,
        //    };
        //    pokemonList.Add(newPokemon);
        //        }
        //icPokemon.ItemsSource = pokemonList;
        //    }

        private void mnuGen1_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}



            // change this logic


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

