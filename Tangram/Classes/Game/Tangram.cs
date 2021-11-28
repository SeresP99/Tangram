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

        public Tan largeTriangle1;
        public Tan largeTriangle2;
        public Tan mediumTriangle;
        public Tan smallTriangle1;
        public Tan smallTriangle2;
        public Tan square;
        public Tan parallelogram;

        public DateTime timeIntoGame;

        public readonly double scale;

        public Tan[] setOfTans;
        public WinConditionChecker winChecker;

        public Tangram(double scale)
        {
            this.scale = scale;
            map = new Map(scale, 25, 25);

            double distance = scale * 1.5;
            largeTriangle1 = new TanTriangle(
                TanTriangleSize.LARGE, Color.Blue,
                (float)(map.X + map.sideA + 10),
                -65,
                scale);


            largeTriangle2 = new TanTriangle(
                TanTriangleSize.LARGE, Color.Orange,
                (float)(map.X + map.sideA + 25),
                -65 + 5,
                scale);

            mediumTriangle = new TanTriangle(
                TanTriangleSize.MEDIUM, Color.Gray,
                (float)(map.X + map.sideA - 40),
                (float)(map.sideA - 125),
                scale);
            mediumTriangle.Rotate();

            square = new TanSquare(
                Color.Orchid,
                mediumTriangle.GetAbsolutePoint('B').X,
                largeTriangle2.GetAbsolutePoint('C').Y - 30,
                scale);
            square.Rotate();

            smallTriangle1 = new TanTriangle(
                TanTriangleSize.SMALL, 
                Color.Purple,
                square.GetAbsolutePoint('D').X + 5,
                square.GetAbsolutePoint('D').Y - 15,
                scale);
            smallTriangle1.Rotate180();

            smallTriangle2 = new TanTriangle(
                TanTriangleSize.SMALL,
                Color.Green,
                square.GetAbsolutePoint('D').X,
                square.GetAbsolutePoint('D').Y - 10,
                scale);
            smallTriangle2.Rotate180();

            parallelogram = new TanParallelogram(
                Color.DeepPink,
                largeTriangle2.GetAbsolutePoint('B').X - 30,
                largeTriangle2.GetAbsolutePoint('B').Y, 
                scale);
            parallelogram.Rotate();
            parallelogram.Rotate();

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
            timeIntoGame = new DateTime(0);
        }

        public Bitmap GetBitmapOfShape(int i)
        {
            return setOfTans[i].bitmap;
        }


    }
}
