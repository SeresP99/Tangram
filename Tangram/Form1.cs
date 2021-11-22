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

namespace TangramProject
{
    public partial class Tangram : Form
    {
        Graphics g;
        Classes.Game.Tangram game;

        bool gotcha = false;
        float dx, dy;
        int grabbedPiece;

        TanTriangle smallTriangle;
        TanTriangle mediumTriangle;
        TanTriangle largeTriangle;

        public Tangram()
        {
            InitializeComponent();
            //square = new TanSquare(Color.Purple, 110, 100, 400);
            smallTriangle = new TanTriangle(TanTriangleSize.SMALL, Color.Red, 100, 100);
            mediumTriangle = new TanTriangle(TanTriangleSize.MEDIUM, Color.Blue, 300, 400);
            largeTriangle = new TanTriangle(TanTriangleSize.LARGE, Color.Green, 700, 400);
            //largeTriangle = new TanTriangle(TanTriangleSize.LARGE, Color.Gold, 110, 300, 400);
            game = new Classes.Game.Tangram(100);
            /*Bitmap temp = game.GetBitmapOfShape(0);*/
        }


        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.DrawImage(smallTriangle.bitmap, smallTriangle.X, smallTriangle.Y);
            g.DrawImage(mediumTriangle.bitmap, mediumTriangle.X, mediumTriangle.Y);
            g.DrawImage(largeTriangle.bitmap, largeTriangle.X, largeTriangle.Y);
            /*for (int i = 0; i < game.setOfTans.Length; i++)
            {
                g.DrawImage(game.setOfTans[i].bitmap, game.setOfTans[i].X, game.setOfTans[i].Y);
            }*/
            /*g.DrawImage(square.bitmap, square.X, square.Y);
            g.DrawImage(smallTriangle.bitmap, smallTriangle.X, smallTriangle.Y);
            g.DrawImage(mediumTriangle.bitmap, mediumTriangle.X, mediumTriangle.Y);
            g.DrawImage(largeTriangle.bitmap, largeTriangle.X, largeTriangle.Y);*/
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    /*for (int i = 0; i < game.setOfTans.Length; i++)
                    {
                        if (game.setOfTans[i].IsPointInArea(e.Location))
                        {
                            gotcha = true;
                            grabbedPiece = i;
                            dx = e.X - game.setOfTans[i].X;
                            dy = e.Y - game.setOfTans[i].Y;
                        }
                    }*/
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
            switch (e.KeyChar)
            {
                case 'r':
                    smallTriangle.Rotate();
                    mediumTriangle.Rotate();
                    largeTriangle.Rotate();
                    canvas.Invalidate();
                    break;
                default:
                    break;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {

        }
    }
}


//ISSUE: resetting a bitmap takes too long and ruins performance
//IDEA: could create a bitmap for each shape and render them one by one on the canvas,
//without: "canvas.Image = (Image)bitmap;" AND using "g.DrawImage(parallelogramBitmap, new Point(X, Y));" instead