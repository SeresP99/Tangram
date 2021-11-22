using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TangramProject.Classes.GraphicsExtensions;
using Tangram.Classes.PointExtensions;

namespace TangramProject.Classes.Pieces
{
    class TanSquare : Tan
    {
        public TanSquare(Color color, float X, float Y)
        {
            this.X = X;
            this.Y = Y;
            this.color = color;
            scale = 98;
            sideA = 1 * scale;

            SetOrResetSquare();
            GetCenter();
            InitializeBitmap();
        }

        private void SetOrResetSquare()
        {
            A = new PointF(0, 0);
            B = new PointF(0, (float)sideA);
            C = new PointF((float)sideA, (float)sideA);
            D = new PointF((float)sideA, 0);

            OffsetSquareForRotation();
        }

        private void OffsetSquareForRotation()
        {
            A.Y += 20;
            B.Y += 20;
            C.Y += 20;
            D.Y += 20;

            A.X += 20;
            B.X += 20;
            C.X += 20;
            D.X += 20;
        }

        private void GetCenter()
        {
            center = new PointF(A.X + (float)sideA / 2, A.Y + (float)sideA / 2);
        }

        private void InitializeBitmap()
        {
            bitmap = new Bitmap((int)sideA + 41, (int)sideA + 41);
            bitmap.DrawTan(this);
        }

        public override bool IsPointInArea(Point p)
        {
            return p.X >= X && p.X <= X + D.X
                    &&
                    p.Y >= Y && p.Y <= Y + B.Y;
        }

        public override void Move(Point cursorLocation, float dx, float dy)
        {
            this.X = cursorLocation.X - dx;
            this.Y = cursorLocation.Y - dy;
            //RefreshPoints();
        }

        public void Rotate()
        {
            float rotationAmount = 45;
            A = A.RotatePoint(center, rotationAmount);
            B = B.RotatePoint(center, rotationAmount);
            C = C.RotatePoint(center, rotationAmount);
            D = D.RotatePoint(center, rotationAmount);

            rotation += rotationAmount;
            if (rotation == 360)
                SetOrResetSquare();

            bitmap.RefreshFrame();
            bitmap.DrawTan(this);
        }

    }
}
