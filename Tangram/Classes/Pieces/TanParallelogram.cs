using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TangramProject.Classes.GraphicsExtensions;

namespace TangramProject.Classes.Pieces
{
    class TanParallelogram : Tan
    {
        public TanParallelogram(Color color, double scale, float X, float Y)
        {
            this.X = X;
            this.Y = Y;
            this.color = color;
            this.scale = scale;
            sideA = Math.Sqrt(2) * scale;
            sideB = 1 * scale;
            CalculateParallelogramAltitude();
            CalculateParallelogramOffset();
            CalculateParallelogramArea();
            A = new PointF((float)offset, 0);
            B = new PointF(0, (float)altitude);
            C = new PointF((float)sideA, (float)altitude);
            D = new PointF((float)(offset + sideA), 0);

            center = new PointF((float)(sideA + offset) / 2 , (float)(altitude / 2));
            bitmap = new Bitmap((int)(sideA + offset) + 10, (int)altitude + 10);
            bitmap.DrawTan(this);
        }

        public double CalculateParallelogramAltitude()
        {
            altitude = Math.Sin(Math.PI / 4) * sideB;
            return altitude;
        }

        public double CalculateParallelogramOffset()
        {

            offset = sideB * sideB - altitude * altitude;
            if (offset < 0)
            {
                offset *= -1;
            }
            offset = Math.Sqrt(offset);
            return offset;
        }

        public double CalculateParallelogramArea()
        {
            return area = sideA * altitude;
        }

        public override bool IsPointInArea(Point p)
        {
            PointF absA = new PointF((float)offset + X, 0 + Y);
            PointF absB = new PointF(0 + X, (float)altitude + Y);
            PointF absC = new PointF((float)sideA + X, (float)altitude + Y);
            PointF absD = new PointF((float)(offset + sideA) + X, 0 + Y);

            float subArea1 = SubTriangleArea(p, absA, absB);
            float subArea2 = SubTriangleArea(p, absB, absC);
            float subArea3 = SubTriangleArea(p, absC, absD);
            float subArea4 = SubTriangleArea(p, absD, absA);
            float difference = subArea1 + subArea2 + subArea3 + subArea4 - (float)area;
            float tolerableDifference = 0.5f;

            return difference < tolerableDifference;
        }

        private float SubTriangleArea(PointF p1, PointF p2, PointF p3)
        {
            return (float)Math.Abs((p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y)) / 2.0);
        }

        public override void Move(Point cursorLocation, float dx, float dy)
        {
            this.X = cursorLocation.X - dx;
            this.Y = cursorLocation.Y - dy;
        }

    }
}
