using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TangramProject.Classes.GraphicsExtensions;

namespace TangramProject.Classes.Pieces
{
    public class TanTriangle : Tan
    {
        //side A is the hypotenuse's length in case of triangle
        //side B is clamps' length

        //2√2 ≈ 2.8

        public TanTriangle(TanTriangleSize type, Color color, double scale, float X, float Y)
        {

            this.type = type;
            this.color = color;
            this.scale = scale;
            this.X = X;
            this.Y = Y;

            switch (this.type)
            {
                case TanTriangleSize.LARGE:
                    sideA = 2 * Math.Sqrt(2) * scale;
                    sideB = 2 * scale;
                    break;

                case TanTriangleSize.MEDIUM:
                    sideA = 2 * scale;
                    sideB = Math.Sqrt(2) * scale;
                    break;

                case TanTriangleSize.SMALL:
                    sideA = Math.Sqrt(2) * scale;
                    sideB = 1 * scale;
                    break;
            }

            CalculateAltitude();
            CalculateArea();
            A = new PointF(0, 0);
            B = new PointF((float)sideA, 0);
            C = new PointF((float)(sideA / 2), (float)altitude);

            OffsetForRotation();
            GetCenter();

            bitmap = new Bitmap((int)sideA + 2, (int)sideA + 2);
            bitmap.RefreshFrame();
            bitmap.DrawTan(this);
        }

        //DONE
        public double CalculateAltitude()
        {
            altitude = (float)(sideB * sideB) / 2;
            return altitude = Math.Sqrt(altitude);
        }

        //DONE
        public double CalculateArea()
        {
            area = (sideA * altitude) / 2;
            return area;
        }

        //DONE
        public override bool IsPointInArea(Point p)
        {
            //calculate location of points relative to the whole board
            PointF absA = new PointF(0 + X, 0 + Y);
            PointF absB = new PointF((float)sideA + X, 0 + Y);
            PointF absC = new PointF((float)(sideA / 2) + X, (float)altitude + Y);

            float subArea1 = SubTriangleArea(p, absB, absC);
            float subArea2 = SubTriangleArea(absA, p, absC);
            float subArea3 = SubTriangleArea(absA, absB, p);

            //type conversion asks for this
            //original: subArea1 + subArea2 + subArea3 == area
            float difference = subArea1 + subArea2 + subArea3 - (float)area;
            float tolerableDifference = 0.5f;

            return difference < tolerableDifference;
        }

        //DONE
        private float SubTriangleArea(PointF p1, PointF p2, PointF p3)
        {
            return (float)Math.Abs((p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y)) / 2.0);
        }

        //DONE
        public override void Move(Point cursorLocation, float dx, float dy)
        {
            this.X = cursorLocation.X - dx;
            this.Y = cursorLocation.Y - dy;
        }

        //DONE
        public void RotateBy90()
        {
            A = RotatePoint(A, center, 90);
            B = RotatePoint(B, center, 90);
            C = RotatePoint(C, center, 90);

            bitmap.RefreshFrame();
            bitmap.DrawTan(this);
        }

        //DONE
        public PointF RotatePoint(PointF pointToRotate, PointF center, double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);


            return new PointF
            {
                X = (int)(cosTheta * (pointToRotate.X - center.X) - sinTheta * (pointToRotate.Y - center.Y) + center.X),
                Y = (int)(sinTheta * (pointToRotate.X - center.X) + cosTheta * (pointToRotate.Y - center.Y) + center.Y)
            };
        }

        //DONE
        private void GetCenter()
        {
            center = new PointF(A.X + (float)sideA / 2, A.Y + (float)altitude / 2);
        }

        //DONE
        public void OffsetForRotation()
        {
            A.Y += 35;
            B.Y += 35;
            C.Y += 35;
        }

    }
}
