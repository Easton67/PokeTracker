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
using static PokeApiNet.PokemonSprites.VersionSprites.GenerationVISprites;

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

        string pokemonLocation;
        string pokeLocation = "Location Not Found";

        public MainWindow()
        {
            InitializeComponent();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int startingDexNumber = 1;
            int endingDexNumber = 151;
            string gameName = "Red";
            EvolutionChain evolution = await pokeClient.GetResourceAsync<EvolutionChain>(1);

            int minLevel = (int)evolution.Chain.EvolvesTo[0]?.EvolutionDetails[0]?.MinLevel.Value;
            MessageBox.Show(minLevel.ToString());

            EvolutionTrigger et = await pokeClient.GetResourceAsync<EvolutionTrigger>(2);
            if (evolution != null && evolution.Chain != null)
            {
                string evolvedPokemonName = evolution.Chain.EvolvesTo[0]?.Species?.Name;
                // int minLevel = (int)(evolution.Chain.EvolutionDetails[1].MinLevel.Value);
                MessageBox.Show(minLevel.ToString());
                if (evolution.Chain.EvolvesTo.Count > 0)
                {
                    string nextEvolutionName = evolution.Chain.EvolvesTo[0]?.EvolvesTo[0]?.Species?.Name;
                    MessageBox.Show(evolvedPokemonName + " & " + nextEvolutionName);
                }
            }
        }

        public async void PokemonItems(int startingDexNumber, int endingDexNumber, string gameName)
        {
            for (int i = startingDexNumber; i <= endingDexNumber; i++)
            {
                PokeApiNet.Pokemon pokemon = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(i);
                string pokeLocation = GetLocation(i, gameName);
                var newPokemon = new DataObjects.Pokemon()
                {
                    
                    PokemonID = 100000,
                    Name = pokemon.Name.ToUpper(),
                    DexNumber = i,
                    Sprite = GetSpritePath(pokemon),
                    CaughtOrNot = false,
                    Location = pokeLocation,
                    Evolution = pokemon.Species.Name
                };
                pokemonList.Add(newPokemon);
            }
            icPokemon.ItemsSource = pokemonList;
        }

        private string GetSpritePath(PokeApiNet.Pokemon pokemon)
        {
            string spritePath = baseDirectory + "sprites\\" + pokemon.Name.ToLower() + "_front_sprite.png";
            return spritePath;
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
                string output = process.StandardOutput.ReadToEnd().Replace("-", " ").ToUpper().Replace("AREA", "").Replace("KANTO", "");

                // Wait for the process to exit
                process.WaitForExit();

                return output;
            }
        }

        private void mnuGen1_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}

