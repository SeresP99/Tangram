using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tangram.Classes.PointExtensions;
using TangramProject.Classes.GraphicsExtensions;

namespace TangramProject.Classes.Pieces
{
    public class TanTriangle : Tan
    {
        //side A is the hypotenuse's length in case of triangle
        //side B is clamps' length

        //2√2 ≈ 2.8

        public TanTriangle(TanTriangleSize type, Color color, float X, float Y)
        {
            scale = 98;
            this.type = type;
            this.color = color;
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
            SetOrResetTriangle();
            GetCenter();
            InitializeBitmap();
        }

        private void SetOrResetTriangle()
        {
            A = new PointF(0, 0);
            B = new PointF((int)sideA, 0);
            C = new PointF((int)(sideA / 2), (int)altitude);

            OffsetTriangleForRotation();
        }

        public void OffsetTriangleForRotation()
        {
            switch (type)
            {
                case TanTriangleSize.LARGE: //DONE'D
                    A.Y += 80;
                    B.Y += 80;
                    C.Y += 80;
                    A.X += 10;
                    B.X += 10;
                    C.X += 10;
                    break;
                case TanTriangleSize.MEDIUM:
                    A.Y += 55;
                    B.Y += 55;
                    C.Y += 55;
                    A.X += 10;
                    B.X += 10;
                    C.X += 10;
                    break;
                case TanTriangleSize.SMALL:
                    A.Y += 40;
                    B.Y += 40;
                    C.Y += 40;
                    A.X += 10;
                    B.X += 10;
                    C.X += 10;
                    break;
                default:
                    break;
            }

        }

        private void InitializeBitmap()
        {
            bitmap = new Bitmap((int)sideA + 20, (int)sideA + 20);
            bitmap.RefreshFrame();
            bitmap.DrawTan(this);
        }

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
            PointF absA = new PointF(A.X + X, A.Y + Y);
            PointF absB = new PointF(B.X + X, B.Y + Y);
            PointF absC = new PointF(C.X + X, C.Y + Y);

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
        public override void Rotate()
        {
            float rotationAmount = 45;
            A = A.RotatePoint(center, rotationAmount);
            B = B.RotatePoint(center, rotationAmount);
            C = C.RotatePoint(center, rotationAmount);

            rotation += rotationAmount;
            if (rotation == 360)
                SetOrResetTriangle();

            bitmap.RefreshFrame();
            bitmap.DrawTan(this);
        }

        //DONE
        

        //DONE
        private void GetCenter()
        {
            center = new PointF(A.X + (float)sideA / 2, A.Y + (float)altitude / 2);
        }

        
        

    }
}
