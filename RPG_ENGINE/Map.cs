using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_ENGINE
{
    public class Map : PictureBox
    {
        List<List<string>> mapData;
        string displayName;

        public Map(string name)
        {
            mapData = new List<List<string>>();

            this.Name = name;
            this.Image = (Image)Properties.Resources.ResourceManager.GetObject(name);
            this.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Location = new Point((800 - this.Width) / 2, (600 - this.Height) / 2);

            string[] str = File.ReadAllLines(name + ".mpmap");
            for (int i = 0; i < str.Length; i++)
            {
                mapData.Add(str[i].Split(';').ToList());
            }
        }

        public List<List<string>> MapData { get { return mapData; } set { mapData = value; } }
        public string DisplayName { get { return displayName; } set { displayName = value; } }
    }
}
