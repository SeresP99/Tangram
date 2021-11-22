using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TangramProject.Classes.GraphicsExtensions;

namespace TangramProject.Classes.Pieces
{
    class TanSquare : Tan
    {
        public TanSquare(Color color, double scale, float X, float Y)
        {
            this.X = X;
            this.Y = Y;
            this.color = color;
            this.scale = scale;
            sideA = 1 * scale;
            A = new PointF(0, 0);
            B = new PointF(0, (float)sideA);
            C = new PointF((float)sideA, (float)sideA);
            D = new PointF((float)sideA, 0);

            center = new PointF((float)sideA / 2, (float)sideA / 2);
            bitmap = new Bitmap((int)sideA + 1, (int)sideA + 1);
            bitmap.DrawTan(this);
        }

        public override bool IsPointInArea(Point p)
        {
            return  p.X >= X && p.X <= X + D.X
                    &&
                    p.Y >= Y && p.Y <= Y + B.Y; 
        }

        public override void Move(Point cursorLocation, float dx, float dy)
        {
            this.X = cursorLocation.X - dx;
            this.Y = cursorLocation.Y - dy;
            //RefreshPoints();
        }

        
    }
}
