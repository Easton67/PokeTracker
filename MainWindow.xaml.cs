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
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PokemonItems();
        }
        private async void PokemonItems()
        {
            int sexnumber = 1;
            //PokeApiNet.Pokemon pokemon = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(sexnumber);
            //List<Move> allMoves = await pokeClient.GetResourceAsync(pokemon.Moves.Select(move => move.Move));
            //MessageBox.Show(allMoves[0].Name);

            //LocationArea locationEncounters = await pokeClient.GetResourceAsync<LocationArea>(1);
            //MessageBox.Show(locationEncounters.Name.ToString());
            //List<PokemonEncounter> pokemonEncounter = await pokeClient.GetResourceAsync<LocationArea>(1);

            //List<PokemonEncounter> pokemonEncounter = locationEncounters.PokemonEncounters.ToList();
            //MessageBox.Show(pokemonEncounter[1].ToString());


            for (int i = 1; i <= 151; i++)
            {
                int dexNumber = i;
                PokeApiNet.Pokemon pokemon = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(dexNumber);

                var newPokemon = new DataObjects.Pokemon()
                {
                    PokemonID = 100000,
                    Name = pokemon.Name.ToUpper(),
                    DexNumber = dexNumber,
                    Sprite = pokemon.Sprites.FrontDefault,
                    CaughtOrNot = false
                };
                pokemonList.Add(newPokemon);
            }
            icPokemon.ItemsSource = pokemonList;
        }
    }
}
