using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_ENGINE
{
    public class Gates
    {
        public string Map { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Target { get; set; }
        public int PlayerX { get; set; }
        public int PlayerY { get; set; }
        public Player.PlayerDirections PlayerDirection { get; set; }
        public int DenemeAbim { get; set; }

        /// <summary>
        /// Bu sınıf oyundaki kapıların bilgilerini tutar
        /// buraya ben yazidim jonhy dcdcdcdc
        /// </summary>
        /// <param name="data"></param>
        public Gates(string[] data)
        {
            Map = data[0];
            X = int.Parse(data[1]);
            Y = int.Parse(data[2]);
            Target = data[3];
            PlayerX = int.Parse(data[4]);
            PlayerY = int.Parse(data[5]);
            PlayerDirection = (Player.PlayerDirections)int.Parse(data[6]);
        }
    }
}
