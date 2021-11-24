using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TangramProject.Classes.Pieces;
using Tangram.Classes.PointExtensions;

namespace Tangram.Classes.Game
{
    class WinConditionChecker
    {
        double scale;
        Map map;
        Tan[] setOfTans;
        PointF[] mapCorners;

        //IDEA: check if shapes are correctly next to each other in a row, one after the other

        //these are all the A points of each shape


        public WinConditionChecker(Tan[] setOfTans, Map map, double scale)
        {
            this.scale = scale;
            this.setOfTans = setOfTans;
            this.map = map;

            mapCorners = new PointF[4]
            {
                new PointF(map.corners[0].X + map.X, map.corners[0].Y + map.Y),
                new PointF(map.corners[1].X + map.X, map.corners[1].Y + map.Y),
                new PointF(map.corners[2].X + map.X, map.corners[2].Y + map.Y),
                new PointF(map.corners[3].X + map.X, map.corners[3].Y + map.Y)
            };
        }

        public bool CheckForWinCondition()
        {
            bool firstArgument = checkLargeTrianglesToMapCorner();
            bool secondArgument = checkLargeTrianglesAgainstEachOther();
            bool thirdArgument = checkLargeTrianglesNotOverlapping();


            return firstArgument && secondArgument && thirdArgument;
        }

        public bool areLargeTrianglesInPlace()
        {
            throw new NotImplementedException();
        }

        private bool checkLargeTrianglesToMapCorner()
        {
            PointF absA = setOfTans[0].GetAbsolutePoint('A');
            foreach (PointF corner in mapCorners)
            {
                if (absA.IsCloseEnoughTo(corner))
                    return true;
            }
            return false;
        }

        private bool checkLargeTrianglesAgainstEachOther()
        {
            PointF absB_0 = setOfTans[0].GetAbsolutePoint('B');
            PointF absA_1 = setOfTans[1].GetAbsolutePoint('A');

            PointF absC_0 = setOfTans[0].GetAbsolutePoint('C');
            PointF absC_1 = setOfTans[1].GetAbsolutePoint('C');

            PointF absA_0 = setOfTans[0].GetAbsolutePoint('A');
            PointF absB_1 = setOfTans[1].GetAbsolutePoint('B');

            return (absB_0.IsCloseEnoughTo(absA_1) || absA_0.IsCloseEnoughTo(absB_1)) && absC_0.IsCloseEnoughTo(absC_1);
        }

        private bool checkLargeTrianglesNotOverlapping()
        {
            PointF absA_0 = setOfTans[0].GetAbsolutePoint('A');
            PointF absA_1 = setOfTans[1].GetAbsolutePoint('A');

            PointF absB_0 = setOfTans[0].GetAbsolutePoint('B');
            PointF absB_1 = setOfTans[1].GetAbsolutePoint('B');


            PointF absC_0 = setOfTans[0].GetAbsolutePoint('C');
            PointF absC_1 = setOfTans[1].GetAbsolutePoint('C');

            return !(absA_0.IsCloseEnoughTo(absA_1) && absB_0.IsCloseEnoughTo(absB_1) && absC_0.IsCloseEnoughTo(absC_1));
        }
    }
}
