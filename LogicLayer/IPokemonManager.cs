using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IPokemonManager
    {
        Task<Pokemon> GetPokemonInformation(int DexNumber);
    }
}
