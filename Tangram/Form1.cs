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

        bool gotcha = false;
        float dx, dy;
        int grabbedPiece;
        //written and tested with:   scale = 215
        double scale = 215;

        TanTriangle triangle;

        public Tangram()
        {
            InitializeComponent();
            triangle = new TanTriangle(TanTriangleSize.LARGE, Color.Red, 100, 100, 141);
        }


        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.DrawImage(triangle.bitmap, triangle.X, triangle.Y);
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (triangle.IsPointInArea(e.Location))
                    {
                        gotcha = true;
                        dx = e.X - triangle.X;
                        dy = e.Y - triangle.Y;
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
                triangle.Move(e.Location, dx, dy);
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
                        triangle.Rotate();
                        canvas.Invalidate();
                        break;
                    default:
                        break;
                }
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