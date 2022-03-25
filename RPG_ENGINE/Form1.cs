using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_ENGINE
{
    public partial class Form1 : Form
    {
        Player player;
        Dictionary<string, Map> map;
        string currentMap = "";
        List<Character> characters;
        Dictionary<string, Actions> actions;
        List<Gates> gates;
        List<Items> items;
        List<string> displayTexts;
        int displayTextsIndex = 0;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (lblDialog.Visible == false)
            {
                if (e.KeyCode == Keys.Left)
                {
                    if (player.PlayerLocation.X > 0)
                    {
                        if (map[currentMap].MapData[player.PlayerLocation.Y][player.PlayerLocation.X - 1] == "1")
                            player.PlayerMove(Player.PlayerDirections.Left);
                        else
                            player.SetPlayerDirection(Player.PlayerDirections.Left);
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (player.PlayerLocation.Y > 0)
                    {
                        if (map[currentMap].MapData[player.PlayerLocation.Y - 1][player.PlayerLocation.X] == "1")
                            player.PlayerMove(Player.PlayerDirections.Up);
                        else
                            player.SetPlayerDirection(Player.PlayerDirections.Up);
                    }
                }
                if (e.KeyCode == Keys.Right)
                {
                    if (player.PlayerLocation.X + 1 < map[currentMap].Width / 32)
                    {
                        if (map[currentMap].MapData[player.PlayerLocation.Y][player.PlayerLocation.X + 1] == "1")
                            player.PlayerMove(Player.PlayerDirections.Right);
                        else
                            player.SetPlayerDirection(Player.PlayerDirections.Right);
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    if (player.PlayerLocation.Y + 1 < map[currentMap].Height / 32)
                    {
                        if (map[currentMap].MapData[player.PlayerLocation.Y + 1][player.PlayerLocation.X] == "1")
                            player.PlayerMove(Player.PlayerDirections.Down);
                        else
                            player.SetPlayerDirection(Player.PlayerDirections.Down);
                    }
                }
            }
            if (e.KeyCode == Keys.A)
            {
                if (lblDialog.Visible)
                    ShowMessage();
                else
                {
                    int controlx = 0, controly = 0;
                    switch (player.PlayerDirection)
                    {
                        case Player.PlayerDirections.Left:
                            controlx = player.PlayerLocation.X - 1;
                            controly = player.PlayerLocation.Y;
                            break;
                        case Player.PlayerDirections.Up:
                            controlx = player.PlayerLocation.X;
                            controly = player.PlayerLocation.Y - 1;
                            break;
                        case Player.PlayerDirections.Right:
                            controlx = player.PlayerLocation.X + 1;
                            controly = player.PlayerLocation.Y;
                            break;
                        case Player.PlayerDirections.Down:
                            controlx = player.PlayerLocation.X;
                            controly = player.PlayerLocation.Y + 1;
                            break;
                        default:
                            break;
                    }

                    if (map[currentMap].MapData[controly][controlx] == "2")
                    {
                        int ci = GetCharacterIndex(controlx, controly);
                        characters[ci].SetCharacterDirection((Character.CharacterDirections)(((int)player.PlayerDirection + 2) % 4));
                        RunAction(characters[ci].Name + "_" + characters[ci].CharacterState);
                    }

                    if (map[currentMap].MapData[controly][controlx] == "3")
                    {
                        Items ci = GetItem(controlx, controly);
                        if (ci != null)
                        {
                            if (ci.State)
                            {
                                RunAction(ci.Name);
                                if (ci.OneTime)
                                    ci.State = false;
                            }
                        }
                    }

                    if (map[currentMap].MapData[controly][controlx] == "5")
                    {
                        Gates gg = GetGate(controlx, controly);
                        if(gg != null)
                        {
                            this.Controls.Remove(map[currentMap]);
                            map[currentMap].Controls.Remove(player);
                            currentMap = gg.Target;
                            player = new Player(gg.PlayerX, gg.PlayerY, gg.PlayerDirection);
                            map[currentMap].Controls.Add(player);
                            this.Controls.Add(map[currentMap]);

                            gates = new List<Gates>();
                            string[] str3 = File.ReadAllLines("gates.mpgates");
                            for (int i = 0; i < str3.Length; i++)
                            {
                                string[] cache = str3[i].Split(';');
                                Gates a = new Gates(cache);
                                if (a.Map == currentMap)
                                    gates.Add(a);
                            }

                            for (int i = 0; i < characters.Count; i++)
                            {
                                if (characters[i].MapName == currentMap)
                                    map[currentMap].Controls.Add(characters[i]);
                            }
                        }
                    }
                }
            }
        }
        int GetCharacterIndex(int x, int y)
        {
            int str = 0;
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i].CharacterLocation.X == x && characters[i].CharacterLocation.Y == y)
                    str = i;
            }
            return str;
        }
        Gates GetGate(int x, int y)
        {
            Gates str = null;
            for (int i = 0; i < gates.Count; i++)
            {
                if (gates[i].X == x && gates[i].Y == y)
                    str = gates[i];
            }
            return str;
        }
        Items GetItem(int x, int y)
        {
            Items str = null;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].X == x && items[i].Y == y)
                    str = items[i];
            }
            return str;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Map _town = new Map("town");
            Map _sinasishome = new Map("sinasishome");
            Map _deryashome = new Map("deryashome");
            Map _didemshome = new Map("didemshome");
            Map _postoffice = new Map("postoffice");
            player = new Player(15, 15, Player.PlayerDirections.Down);
            player.Visible = false;
            _town.Controls.Add(player);

            _town.Paint += _town_Paint;
            this.Controls.Add(_town);
            currentMap = "town";
            map = new Dictionary<string, Map>();
            map.Add("town", _town);
            map.Add("sinasishome", _sinasishome);
            map.Add("deryashome", _deryashome);
            map.Add("didemshome", _didemshome);
            map.Add("postoffice", _postoffice);


            items = new List<Items>();
            string[] str4 = File.ReadAllLines("items.mpitems");
            for (int i = 0; i < str4.Length; i++)
            {
                items.Add(new Items(str4[i].Split(';')));
            }

            characters = new List<Character>();
            string[] str = File.ReadAllLines("characters.mpchar");
            for (int i = 0; i < str.Length; i++)
            {
                characters.Add(new Character(str[i].Split(';')[1], str[i].Split(';')[2], int.Parse(str[i].Split(';')[3]), int.Parse(str[i].Split(';')[4]), (Character.CharacterDirections)int.Parse(str[i].Split(';')[5]), str[i].Split(';')[0]));
                if (str[i].Split(';')[0] == "town")
                    _town.Controls.Add(characters.Last());
            }

            actions = new Dictionary<string, Actions>();
            string[] str2 = File.ReadAllLines("actions.mpact");
            for (int i = 0; i < str2.Length; i++)
            {
                string[] cache = str2[i].Split('|');
                Actions a = new Actions(cache);
                actions.Add(cache[0], a);
            }

            gates = new List<Gates>();
            string[] str3 = File.ReadAllLines("gates.mpgates");
            for (int i = 0; i < str3.Length; i++)
            {
                string[] cache = str3[i].Split(';');
                Gates a = new Gates(cache);
                if (a.Map == "town")
                    gates.Add( a);
            }
        }

        private void _town_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(player.Image, new Rectangle(player.Location.X, player.Location.Y, player.Width, player.Height));
        }

        void RunAction(string name)
        {
            if (actions.ContainsKey(name))
            {
                switch (actions[name].ActionType)
                {
                    case Actions.ActionTypes.ShowText:
                        actions[name].Text = actions[name].Text.Replace(">>>", "|");
                        displayTexts = actions[name].Text.Split('|').ToList();
                        displayTextsIndex = 0;
                        ShowMessage();
                        break;
                    case Actions.ActionTypes.BaseStateUpdate:
                        SetCharacterState(actions[name].Name.Split('_')[0], actions[name].BaseStateIndex);
                        break;
                    case Actions.ActionTypes.ObjectStateUpdate:
                        SetCharacterState(actions[name].StateObjectName, actions[name].ObjectStateIndex);
                        break;
                    case Actions.ActionTypes.MoveObject:
                        SetCharacterLocation(actions[name].MoveObjectName.Split('_')[0], actions[name].OffsetX, actions[name].OffsetY);
                        break;
                    case Actions.ActionTypes.ShowText_BaseStateUpdate_MoveObject:
                        actions[name].Text = actions[name].Text.Replace(">>>", "|");
                        displayTexts = actions[name].Text.Split('|').ToList();
                        displayTextsIndex = 0;
                        ShowMessage();
                        SetCharacterState(actions[name].Name.Split('_')[0], actions[name].BaseStateIndex);
                        SetCharacterLocation(actions[name].MoveObjectName.Split('_')[0], actions[name].OffsetX, actions[name].OffsetY);
                        break;
                    case Actions.ActionTypes.ShowText_BaseStateUpdate:
                        actions[name].Text = actions[name].Text.Replace(">>>", "|");
                        displayTexts = actions[name].Text.Split('|').ToList();
                        displayTextsIndex = 0;
                        ShowMessage();
                        SetCharacterState(actions[name].Name.Split('_')[0], actions[name].BaseStateIndex);
                        break;
                    case Actions.ActionTypes.ShowText_BaseStateUpdate_ObjectStateUpdate:
                        actions[name].Text = actions[name].Text.Replace(">>>", "|");
                        displayTexts = actions[name].Text.Split('|').ToList();
                        displayTextsIndex = 0;
                        ShowMessage();
                        SetCharacterState(actions[name].Name.Split('_')[0], actions[name].BaseStateIndex);
                        SetCharacterState(actions[name].StateObjectName, actions[name].ObjectStateIndex);
                        break;
                    case Actions.ActionTypes.ShowText_ObjectStateUpdate:
                        actions[name].Text = actions[name].Text.Replace(">>>", "|");
                        displayTexts = actions[name].Text.Split('|').ToList();
                        displayTextsIndex = 0;
                        ShowMessage();
                        SetCharacterState(actions[name].StateObjectName, actions[name].ObjectStateIndex);
                        break;
                }
            }
        }

        void SetCharacterState(string character, int state)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i].Name == character && characters[i].CharacterState < state)
                {
                    characters[i].CharacterState = state;
                }
            }
        }

        void SetCharacterLocation(string character, int ox, int oy)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i].Name == character)
                {
                    int _x = characters[i].CharacterLocation.X;
                    int _y = characters[i].CharacterLocation.Y;
                    characters[i].CharacterMove(ox, oy);
                    SwapMapData(_x, _y, characters[i].CharacterLocation.X, characters[i].CharacterLocation.Y);
                }
            }
        }

        void SwapMapData(int x, int y, int x2, int y2)
        {
            string c = map[currentMap].MapData[y][x];
            map[currentMap].MapData[y][x] = map[currentMap].MapData[y2][x2];
            map[currentMap].MapData[y2][x2] = c;
        }

        void ShowMessage()
        {
            if (displayTextsIndex < displayTexts.Count)
            {
                lblDialog.Text = displayTexts[displayTextsIndex];
                lblDialog.Visible = true;
                displayTextsIndex += 1;
            }
            else
                lblDialog.Visible = false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
