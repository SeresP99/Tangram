using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TangramProject.Classes.Pieces
{
    public abstract class Tan
    {
        public TanTriangleSize type;

        //side A is the hypotenuse's length in case of triangle
        //side B is clamps' length
        public Bitmap bitmap;
        public double sideA;
        public double sideB;
        public double rotation;

        public float X, Y;

        public PointF A;
        public PointF B;
        public PointF C;
        public PointF D;
        public PointF center;

        public double altitude;
        public double area;

        public float offsetWithinBitmap;

        public double scale;
        public Color color;

        public abstract bool IsPointInArea(Point p);
        public abstract void Move(Point cursorLocation, float dx, float dy);
        public abstract void Rotate();
        public PointF GetAbsolutePoint(char pointLetter)
        {
            switch (pointLetter)
            {
                case 'A':
                    return new PointF(X + A.X, Y + A.Y);
                case 'B':
                    return new PointF(X + B.X, Y + B.Y);
                case 'C':
                    return new PointF(X + C.X, Y + C.Y);
                case 'D':
                    return new PointF(X + D.X, Y + D.Y);
                default:
                    throw new Exception("that's a nono");
            }
        }
        //2√2 ≈ 2.8

        /*public Tan(TanType type, Color color, double scale, float X, float Y)
        {
            this.type = type;
            this.X = X;
            this.Y = Y;
            switch (type)
            {
                case TanType.LARGE_TRIANGLE:
                    sideA = 2 * Math.Sqrt(2) * scale;
                    sideB = 2 * scale;
                    CalculateTriangleAltitude();
                    A = new PointF(0 + X, 0 + Y);
                    B = new PointF((float)sideA + X, 0 + Y);
                    C = new PointF((float)(sideA / 2) + X, (float)altitude + Y);
                    break;

                case TanType.MEDIUM_TRIANGLE:
                    sideA = 2 * scale;
                    sideB = Math.Sqrt(2) * scale;
                    CalculateTriangleAltitude();
                    A = new PointF(0 + X, 0 + Y);
                    B = new PointF((float)sideA + X, 0 + Y);
                    C = new PointF((float)(sideA / 2) + X, (float)altitude + Y);
                    break;

                case TanType.SMALL_TRIANGLE:
                    sideA = Math.Sqrt(2) * scale;
                    sideB = 1 * scale;
                    CalculateTriangleAltitude();
                    A = new PointF(0 + X, 0 + Y);
                    B = new PointF((float)sideA + X, 0 + Y);
                    C = new PointF((float)(sideA / 2) + X, (float)altitude + Y);
                    break;

                case TanType.SQUARE:
                    sideA = 1 * scale;
                    A = new PointF(0 + X, 0 + Y);
                    B = new PointF((float)sideA + X, 0 + Y);
                    C = new PointF((float)sideA + X, (float)sideA + Y);
                    D = new PointF(0 + X, (float)sideA + Y);
                    break;

                case TanType.PARALLELOGRAM:
                    sideA = Math.Sqrt(2) * scale;
                    sideB = 1 * scale;
                    CalculateParallelogramAltitude();
                    CalculateParallelogramOffset();
                    A = new PointF(0 + X, 0 + Y);
                    B = new PointF((float)-offset + X, (float)altitude + Y);
                    C = new PointF((float)sideA - (float)offset + X, (float)altitude + Y);
                    D = new PointF((float)sideA + X, 0 + Y);
                    break;
            }

            this.color = color;
        }

        public double CalculateParallelogramAltitude()
        {
            altitude = Math.Sin(Math.PI / 4) * sideB;
            return altitude;
        }

        public double CalculateParallelogramOffset()
        {

            offset = altitude * altitude - sideB * sideB;
            if (offset < 0)
            {
                offset *= -1;
            }
            offset = Math.Sqrt(offset);
            return offset;
        }

        public double CalculateTriangleAltitude()
        {
            //(sideB * sideB) + (altitude * altitude) = sideA * sideA
            altitude = (sideB * sideB) - ((sideA / 2) * (sideA / 2));
            return altitude = Math.Sqrt(altitude);
        }*/
    }
}
