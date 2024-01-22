using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class PokemonManager : IPokemonManager
    {
        // dependency inversion for the data provider
        private IPokemonAccessor _pokemonAccessor = null;

        public PokemonManager()
        {
            _pokemonAccessor = new PokemonAccessor();
        }

        // the optional constructor can accept any data provider
        public PokemonManager(PokemonAccessor pokemonAccessor)
        {
             _pokemonAccessor = pokemonAccessor;
        }

        public async Task<Pokemon> GetPokemonInformation(int DexNumber)
        {
            Pokemon pokemon = null;

            try
            {
                pokemon = await _pokemonAccessor.GetPokemonInformationAsync(DexNumber);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving Pokemon information", ex);
            }
            return pokemon;
        }
    }
}
