using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_ENGINE
{
    public class Character : PictureBox
    {
        public enum CharacterDirections { Left = 0, Up = 1, Right = 2, Down = 3 }

        Point characterLocation;
        Dictionary<string, Image> textures;
        CharacterDirections characterDirections;
        int characterState;
        string displayName;
        string mapName;

        public Character(string name, string dname, int x, int y, CharacterDirections direction, string mapname)
        {
            characterLocation = new Point(x, y);
            textures = new Dictionary<string, Image>();
            textures.Add("Left", (Image)Properties.Resources.ResourceManager.GetObject(name + "_1"));
            textures.Add("Up", (Image)Properties.Resources.ResourceManager.GetObject(name + "_2"));
            textures.Add("Right", (Image)Properties.Resources.ResourceManager.GetObject(name + "_3"));
            textures.Add("Down", (Image)Properties.Resources.ResourceManager.GetObject(name + "_4"));
            characterDirections = direction;
            displayName = dname;
            mapName = mapname;
            characterState = 0;
            this.Image = textures[characterDirections.ToString()];
            this.Location = new Point(x * 32, y * 32);
            this.BackColor = Color.Transparent;
            this.Name = name;
            this.Size = new Size(32, 32);
        }

        public Point CharacterLocation { get { return characterLocation; } set { characterLocation = value; } }
        public CharacterDirections CharacterDirection { get { return characterDirections; } set { characterDirections = value; } }
        public int CharacterState { get { return characterState; } set { characterState = value; } }
        public string DisplayName { get { return displayName; } set { displayName = value; } }
        public string MapName { get { return mapName; } set { mapName = value; } }

        public void CharacterMove(int offsetx, int offsety)
        {
            characterLocation.X += offsetx;
            characterLocation.Y += offsety;
            this.Location = new Point((int)characterLocation.X * 32, (int)characterLocation.Y * 32);
        }

        public void SetCharacterDirection(CharacterDirections directions)
        {
            characterDirections = directions;
            Image = textures[characterDirections.ToString()];
        }
    }
}
