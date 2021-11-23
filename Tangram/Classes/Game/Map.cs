using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TangramProject.Classes.GraphicsExtensions;

namespace Tangram.Classes.Game
{
    class Map
    {
        public double X;
        public double Y;

        public PointF A;
        public PointF B;
        public PointF C;
        public PointF D;

        double sideA;

        public Bitmap bitmap;

        public Map(double scale, double X, double Y)
        {
            this.X = X;
            this.Y = Y;

            sideA = Math.Sqrt(2) * 2 * scale;

            A = new PointF(0, 0);
            B = new PointF((float)sideA, 0);
            C = new PointF((float)sideA, (float)sideA);
            D = new PointF(0, (float)sideA);

            InitializeBitmap();
        }

        public void InitializeBitmap()
        {
            bitmap = new Bitmap((int)sideA + 10, (int)sideA + 10);
            bitmap.DrawMap(this);
        }

    }
}
