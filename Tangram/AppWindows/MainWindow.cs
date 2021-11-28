using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TangramProject.Classes.Pieces;
using TangramProject.Classes.GraphicsExtensions;
using TangramProject.Classes.Game;
using System.Threading;
using Tangram.AppWindows;

namespace TangramProject
{
    public partial class Tangram : Form
    {
        Graphics g;

        bool gotcha = false;
        float dx, dy;
        int grabbedPiece;

        double scale = 100;

        Classes.Game.Tangram game;

        public Tangram()
        {
            SizeSettingWindow settingWindow = new SizeSettingWindow(scale, this);
            settingWindow.ShowDialog();
            scale = settingWindow.scale;
            InitializeComponent();
            setSize();
            game = new Classes.Game.Tangram(scale);
        }


        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.DrawImage(game.map.bitmap, game.map.X, game.map.Y);
            for (int i = 0; i < game.setOfTans.Length; i++)
            {
                g.DrawImage(game.setOfTans[i].bitmap, game.setOfTans[i].X, game.setOfTans[i].Y);
            }
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    for (int i = 0; i < game.setOfTans.Length; i++)
                    {
                        if (game.setOfTans[i].IsPointInArea(e.Location))
                        {
                            gotcha = true;
                            grabbedPiece = i;
                            dx = e.X - game.setOfTans[i].X;
                            dy = e.Y - game.setOfTans[i].Y;
                        }
                    }
                    break;
                case MouseButtons.Right:
                    break;
                case MouseButtons.Middle:
                    break;
                default:
                    break;
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    gotcha = false;
                    if (game.winChecker.CheckForWinCondition())
                        MessageBox.Show("You have succeeded!" + "\n" + "Time you took: " + timer1.ToString());
                    else
                        label1.Text = "Checking...";
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    break;
                default:
                    break;
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (gotcha)
            {
                game.setOfTans[grabbedPiece].Move(e.Location, dx, dy);
                canvas.Invalidate();
            }
        }

        private void RotateKeypress(object sender, KeyPressEventArgs e)
        {
            if (gotcha)
            {
                switch (e.KeyChar)
                {
                    case 'r':
                        game.setOfTans[grabbedPiece].Rotate();
                        canvas.Invalidate();
                        break;
                    case 'f':
                        break;
                    default:
                        break;
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            game.timeIntoGame.AddSeconds(1);
        }

        private void setSize()
        {
            Width = (int)(scale * (Width / 100));
            Height = (int)(scale * (Height / 100));
            Size = new Size(Width, Height);
            canvas.Width = Width;
            canvas.Height = Height;
        }

    }
}


//ISSUE: resetting a bitmap takes too long and ruins performance
//IDEA: could create a bitmap for each shape and render them one by one on the canvas,
//without: "canvas.Image = (Image)bitmap;" AND using "g.DrawImage(parallelogramBitmap, new Point(X, Y));" instead