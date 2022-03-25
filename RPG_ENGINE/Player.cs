using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_ENGINE
{
    public class Player : PictureBox
    {
        Point playerLocation;
        Timer timer = new Timer();
        int sequence = 4;
        public enum PlayerDirections { Left = 0, Up = 1, Right = 2, Down = 3 }
        PlayerDirections playerDirection;
        Dictionary<string, Image[]> textures;
        bool isPlayable;

        public Player()
        {
            textures = new Dictionary<string, Image[]>();
            textures.Add("Left", new Image[] { Properties.Resources._7, Properties.Resources._8 });
            textures.Add("Up", new Image[] { Properties.Resources._3, Properties.Resources._4 });
            textures.Add("Right", new Image[] { Properties.Resources._5, Properties.Resources._6 });
            textures.Add("Down", new Image[] { Properties.Resources._1, Properties.Resources._2 });
            this.Size = new System.Drawing.Size(32, 32);
            this.BackColor = Color.Transparent;
            this.Location = new Point(0, 0);
            this.Image = Properties.Resources._7;
            timer.Interval = 50;
            playerDirection = PlayerDirections.Down;
            timer.Tick += Timer_Tick;
            isPlayable = true;
            playerLocation = new Point(0, 0);
        }

        public Player(int x, int y, PlayerDirections direction)
        {
            textures = new Dictionary<string, Image[]>();
            textures.Add("Left", new Image[] { Properties.Resources._7, Properties.Resources._8 });
            textures.Add("Up", new Image[] { Properties.Resources._3, Properties.Resources._4 });
            textures.Add("Right", new Image[] { Properties.Resources._5, Properties.Resources._6 });
            textures.Add("Down", new Image[] { Properties.Resources._1, Properties.Resources._2 });
            this.Size = new System.Drawing.Size(32, 32);
            this.BackColor = Color.Transparent;
            this.Location = new Point(x * 32, y * 32);
            this.Image = textures[direction.ToString()][0];
            timer.Interval = 50;
            playerDirection = direction;
            timer.Tick += Timer_Tick;
            isPlayable = true;
            playerLocation = new Point(x, y);
        }

        public void PlayerMove(PlayerDirections direction)
        {
            if(isPlayable)
            {
                isPlayable = false;
                playerDirection = direction;
                switch (direction)
                {
                    case PlayerDirections.Left:
                        playerLocation.X -= 1;
                        break;
                    case PlayerDirections.Up:
                        playerLocation.Y -= 1;
                        break;
                    case PlayerDirections.Right:
                        playerLocation.X += 1;
                        break;
                    case PlayerDirections.Down:
                        playerLocation.Y += 1;
                        break;
                    default:
                        break;
                }
                timer.Enabled = true;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            sequence -= 1;
            this.Image = textures[playerDirection.ToString()][sequence % 2];
            if (playerDirection == PlayerDirections.Left)
            {
                this.Left -= 8;
                this.Parent.Left += 8;
            }
            if (playerDirection == PlayerDirections.Up)
            {
                this.Top -= 8;
                this.Parent.Top += 8;
            }
            if (playerDirection == PlayerDirections.Right)
            {
                this.Left += 8;
                this.Parent.Left -= 8;
            }
            if (playerDirection == PlayerDirections.Down)
            {
                this.Top += 8;
                this.Parent.Top -= 8;
            }
            this.Parent.Refresh();
            if (sequence == 0)
            {
                timer.Enabled = false;
                sequence = 4;
                isPlayable = true;
            }
        }

        public void SetPlayerDirection(PlayerDirections direction)
        {
            if(isPlayable)
            {
                isPlayable = false;
                playerDirection = direction;
                this.Image = textures[playerDirection.ToString()][0];
                isPlayable = true;
            }
        }

        public Point PlayerLocation { get { return playerLocation; } }
        public PlayerDirections PlayerDirection { get { return playerDirection; } set { playerDirection = value; } }
    }
}
