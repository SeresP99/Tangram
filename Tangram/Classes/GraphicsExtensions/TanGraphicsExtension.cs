using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TangramProject.Classes.Pieces;

namespace TangramProject.Classes.GraphicsExtensions
{
    static class TanGraphicsExtension
    {

        public static void DrawTan(this Bitmap bitmap, Tan shape)
        {
                if (shape is TanSquare || shape is TanParallelogram)
                {
                    bitmap.DrawQuadrilateral(shape);
                }
                else
                {
                    bitmap.DrawTriangle(shape);
                }
                //bitmap.FillTan(shape);
        }

        static void DrawQuadrilateral(this Bitmap bitmap, Tan quadrilateral)
        {
            bitmap.DrawLineMidpoint(quadrilateral.color, Point.Round(quadrilateral.A), Point.Round(quadrilateral.B));
            bitmap.DrawLineMidpoint(quadrilateral.color, Point.Round(quadrilateral.B), Point.Round(quadrilateral.C));
            bitmap.DrawLineMidpoint(quadrilateral.color, Point.Round(quadrilateral.C), Point.Round(quadrilateral.D));
            bitmap.DrawLineMidpoint(quadrilateral.color, Point.Round(quadrilateral.D), Point.Round(quadrilateral.A));
        }

        static void DrawTriangle(this Bitmap bitmap, Tan triangle)
        {
            bitmap.DrawLineMidpoint(triangle.color, Point.Round(triangle.A), Point.Round(triangle.B));
            bitmap.DrawLineMidpoint(triangle.color, Point.Round(triangle.B), Point.Round(triangle.C));
            bitmap.DrawLineMidpoint(triangle.color, Point.Round(triangle.C), Point.Round(triangle.A));
        }

        static void FillTan(this Bitmap bitmap, Tan shape)
        {
            bitmap.StackFill(Point.Round(shape.center), shape.color);
        }

    }
}
