using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    [Serializable]
    public class Pokemon
    {
        public int PokemonID { get; set; }
        public string Name { get; set; }
        public int DexNumber { get; set; }
        public string Sprite { get; set; }
        public bool CaughtOrNot { get; set; }
    }
}
