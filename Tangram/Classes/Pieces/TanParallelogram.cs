using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TangramProject.Classes.GraphicsExtensions;
using Tangram.Classes.PointExtensions;
using System.Windows.Forms;

namespace TangramProject.Classes.Pieces
{
    class TanParallelogram : Tan
    {
        double offset;

        public TanParallelogram(Color color, float X, float Y, double scale)
        {
            this.scale = scale;
            this.X = X;
            this.Y = Y;
            this.color = color;
            sideA = Math.Sqrt(2) * scale;
            sideB = 1 * scale;

            CalculateParallelogramAltitude();
            CalculateParallelogramOffset();
            CalculateParallelogramArea();
            SetOrResetParallelogram();
            GetCenter();
            InitializeBitmap();
        }

        private void SetOrResetParallelogram()
        {
            A = new PointF((float)offset, 0);
            B = new PointF(0, (float)altitude);
            C = new PointF((float)sideA, (float)altitude);
            D = new PointF((float)(offset + sideA), 0);

            GetCenter();
            OffsetParallelogramForRotation();
        }

        private void OffsetParallelogramForRotation()
        {
            A.Y += 70;
            B.Y += 70;
            C.Y += 70;
            D.Y += 70;
            A.X += 10;
            B.X += 10;
            C.X += 10;
            D.X += 10;
        }

        private void InitializeBitmap()
        {
            bitmap = new Bitmap((int)(sideA + offset) + 70, (int)(sideA + offset) + 70);
            bitmap.DrawTan(this);
        }

        private void GetCenter()
        {
            center = new PointF(B.X + (float)(sideA + offset) / 2, B.Y - (float)(altitude / 2));
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
            PointF absA = new PointF(A.X + X, A.Y + Y);
            PointF absB = new PointF(B.X + X, B.Y + Y);
            PointF absC = new PointF(C.X + X, C.Y + Y);
            PointF absD = new PointF(D.X + X, D.Y + Y);

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

        public override void Rotate()
        {
            float rotationAmount = 45;
            A = A.RotatePoint(center, rotationAmount);
            B = B.RotatePoint(center, rotationAmount);
            C = C.RotatePoint(center, rotationAmount);
            D = D.RotatePoint(center, rotationAmount);

            rotation += rotationAmount;
            if (rotation == 360)
                SetOrResetParallelogram();

            bitmap.RefreshFrame();
            try
            {
                bitmap.DrawTan(this);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " \nFailure at shape: " + this.GetType().Name);
            }
        }
    }
}
