using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_ENGINE
{
    public class Items
    {
        public string Map { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool State { get; set; }
        public bool OneTime { get; set; }

        public Items(string[] data)
        {
            Map = data[0];
            Name = data[1];
            X = int.Parse(data[2]);
            Y = int.Parse(data[3]);
            State = true;
            OneTime = bool.Parse(data[4]);
        }
    }
}
