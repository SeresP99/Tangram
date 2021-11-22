using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TangramProject.Classes.Pieces;
using TangramProject.Classes.GraphicsExtensions;

namespace TangramProject.Classes.Game
{
    /// <summary>
    /// https://upload.wikimedia.org/wikipedia/commons/2/21/Tangram_paradox_explanation.svg
    /// </summary>
    class Tangram
    {
        Tan largeTriangle1;
        Tan largeTriangle2;
        Tan mediumTriangle1;
        Tan mediumTriangle2;
        Tan smallTriangle1;
        Tan smallTriangle2;
        Tan square;
        Tan parallelogram;

        public readonly double scale;

        public Tan[] setOfTans;

        public Tangram(double scale)
        {
            this.scale = scale;
            largeTriangle1 = new TanTriangle(TanTriangleSize.LARGE, Color.Blue, 997, 629);
            largeTriangle2 = new TanTriangle(TanTriangleSize.LARGE, Color.Orange, 1000, 461);
            mediumTriangle1 = new TanTriangle(TanTriangleSize.MEDIUM, Color.Gray, 1047, 334);
            mediumTriangle2 = new TanTriangle(TanTriangleSize.MEDIUM, Color.Yellow, 1052, 216);
            smallTriangle1 = new TanTriangle(TanTriangleSize.SMALL, Color.Purple, 1083, 33);
            smallTriangle2 = new TanTriangle(TanTriangleSize.SMALL, Color.Green, 1087, 131);
            square = new TanSquare(Color.Orchid, 846, 420);
            parallelogram = new TanParallelogram(Color.DeepPink, 829, 668);

            setOfTans = new Tan[8]
            {
                largeTriangle1,
                largeTriangle2,
                mediumTriangle1,
                mediumTriangle2,
                smallTriangle1,
                smallTriangle2,
                square,
                parallelogram
            };

            //DrawBoard();
        }

        public Bitmap GetBitmapOfShape(int i)
        {
            return setOfTans[i].bitmap;
        }


    }
}
