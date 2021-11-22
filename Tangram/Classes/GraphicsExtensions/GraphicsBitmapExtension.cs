using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TangramProject.Classes.Pieces;

namespace TangramProject.Classes.GraphicsExtensions
{
    static class GraphicsBitmapExtension
    {

        public static void DrawLineMidpoint(this Bitmap bitmap, Color color, Point p1, Point p2)
        {
            int R;
            bool T;
            int D;
            int X, Y;

            int dx = Math.Abs(p2.X - p1.X);
            int dy = Math.Abs(p2.Y - p1.Y);
            int sx = Math.Sign(p2.X - p1.X);
            int sy = Math.Sign(p2.Y - p1.Y);

            if (dx < dy)
            {
                R = dx;
                dx = dy;
                dy = R;
                T = true;
            }
            else T = false;

            D = 2 * dy - dx;
            X = p1.X;
            Y = p1.Y;
            bitmap.SetPixel(X, Y, color);
            while ((X != p2.X) || (Y != p2.Y))
            {
                if (D > 0)
                {
                    if (T)
                    {
                        X = X + sx;
                    }
                    else
                    {
                        Y = Y + sy;
                    }
                    D = D - 2 * dx;
                }

                if (T)
                    Y = Y + sy;
                else
                    X = X + sx;
                D = D + 2 * dy;
                bitmap.SetPixel(X, Y, color);
            }
        }

        /*public static void StackFill(this Bitmap bitmap, Tan tan)
        {
            bitmap.StackFill(Point.Round(tan.center), tan.color);
        }*/

        public static void StackFill(this Bitmap bitmap, Point P, Color color)
        {
            int[] dx = new int[] { 0, 1, 0, -1 };
            int[] dy = new int[] { 1, 0, -1, 0 };
            Stack<Point> stack = new Stack<Point>();
            stack.Push(P);
            Point p0, p1;
            while (stack.Count > 0)
            {
                p0 = stack.Pop();
                bitmap.SetPixel(p0.X, p0.Y, color);
                for (int i = 0; i < 4; i++)
                {
                    p1 = new Point(p0.X + dx[i], p0.Y + dy[i]);
                    if (!bitmap.GetPixel(p1.X, p1.Y).ColorIsTheSameAs(color))
                        stack.Push(p1);
                }
            }
        }


        public static void RefreshFrame(this Bitmap bitmap)
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    bitmap.SetPixel(i, j, Color.FromArgb(0, 0, 0, 0));
                }
            }
        }

        public static bool ColorIsTheSameAs(this Color color1, Color color2)
        {
            return color1.R == color2.R &&
                    color1.G == color2.G &&
                    color1.B == color2.B;
        }


    }
}
