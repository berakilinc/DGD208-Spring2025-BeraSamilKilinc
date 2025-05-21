using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSProjectFile
{
    public class Pet
    {
        public string Name { get; set; }
        public PetType Type { get; set; }
        public int Hunger { get; set; } = 50;
        public int Sleep { get; set; } = 50;
        public int Fun { get; set; } = 50;
        public bool IsAlive => Hunger > 0 && Sleep > 0 && Fun > 0;

        public Pet(string name, PetType type)
        {
            Name = name;
            Type = type;
        }
    }
}
