using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TangramProject.Classes.Pieces;
using TangramProject.Classes.GraphicsExtensions;
using Tangram.Classes.Game;

namespace TangramProject.Classes.Game
{
    /// <summary>
    /// https://upload.wikimedia.org/wikipedia/commons/2/21/Tangram_paradox_explanation.svg
    /// </summary>
    class Tangram
    {
        public Map map;

        Tan largeTriangle1;
        Tan largeTriangle2;
        Tan mediumTriangle;
        Tan smallTriangle1;
        Tan smallTriangle2;
        Tan square;
        Tan parallelogram;

        public readonly double scale;

        public Tan[] setOfTans;
        public WinConditionChecker winChecker;

        public Tangram(double scale)
        {
            this.scale = scale;
            map = new Map(scale, 25, 25);
            largeTriangle1 = new TanTriangle(TanTriangleSize.LARGE, Color.Blue, 50, 50, scale);
            largeTriangle2 = new TanTriangle(TanTriangleSize.LARGE, Color.Orange, 50, 50, scale);
            mediumTriangle = new TanTriangle(TanTriangleSize.MEDIUM, Color.Gray, 50, 200, scale);
            smallTriangle1 = new TanTriangle(TanTriangleSize.SMALL, Color.Purple, 50, 400, scale);
            smallTriangle2 = new TanTriangle(TanTriangleSize.SMALL, Color.Green, 50, 400, scale);
            square = new TanSquare(Color.Orchid, 400, 50, scale);
            parallelogram = new TanParallelogram(Color.DeepPink, 400, 200, scale);

            setOfTans = new Tan[7]
            {
                largeTriangle1,
                largeTriangle2,
                mediumTriangle,
                smallTriangle1,
                smallTriangle2,
                square,
                parallelogram
            };

            winChecker = new WinConditionChecker(setOfTans, map, scale);
        }

        public Bitmap GetBitmapOfShape(int i)
        {
            return setOfTans[i].bitmap;
        }


    }
}
