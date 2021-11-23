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
            largeTriangle1 = new TanTriangle(TanTriangleSize.LARGE, Color.Blue, 50, 50);
            largeTriangle2 = new TanTriangle(TanTriangleSize.LARGE, Color.Orange, 50, 50);
            mediumTriangle1 = new TanTriangle(TanTriangleSize.MEDIUM, Color.Gray, 50, 200);
            mediumTriangle2 = new TanTriangle(TanTriangleSize.MEDIUM, Color.Yellow, 50, 200);
            smallTriangle1 = new TanTriangle(TanTriangleSize.SMALL, Color.Purple, 50, 400);
            smallTriangle2 = new TanTriangle(TanTriangleSize.SMALL, Color.Green, 50, 400);
            square = new TanSquare(Color.Orchid, 400, 50);
            parallelogram = new TanParallelogram(Color.DeepPink, 400, 200);

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
        }

        public Bitmap GetBitmapOfShape(int i)
        {
            return setOfTans[i].bitmap;
        }


    }
}
