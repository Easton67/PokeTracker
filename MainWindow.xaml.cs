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
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<DataObjects.Pokemon> pokemonList = new List<DataObjects.Pokemon>();
            string spriteFolder = baseDirectory + "sprites\\";
            var newPokemon = new DataObjects.Pokemon()
            {
                PokemonID = 100000,
                Name = "Zekrom",
                DexNumber = 1,
                Sprite = spriteFolder + "Zekrom_front_sprite.png",
                CaughtOrNot = true
            };
            pokemonList.Add(newPokemon);
            pokemonList.Add(newPokemon);
            pokemonList.Add(newPokemon);
            pokemonList.Add(newPokemon);
            pokemonList.Add(newPokemon);
            pokemonList.Add(newPokemon);
            icPokemon.ItemsSource = pokemonList;
        }
        private async void btnPokemonNumber_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int DexNumber = int.Parse(txtPokemonNumber.Text);
                DataObjects.Pokemon pokemonResult = _pokemonManager.GetPokemonInformation(DexNumber).Result;
                CurrentPokemon = pokemonResult;
                MessageBox.Show("Finished");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            //// instantiate client
            //PokeApiClient pokeClient = new PokeApiClient();

            //// get a resource by name
            //PokeApiNet.Pokemon Pikachu = await pokeClient.GetResourceAsync<PokeApiNet.Pokemon>("Pikachu");
            //MessageBox.Show(Pikachu.Name);
        }
    }
}
