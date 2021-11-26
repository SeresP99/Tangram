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

        int largeTriangleCornerNumber;
        int oppositeCorner;

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
            bool largeTriangles = areLargeTrianglesInPlace();
            bool mediumTriangle = isMediumTriangleInPlace();
            bool parallelogram = isParallelogramInPlace();
            bool smallTriangle = areSmallTrianglesInPlace();
            bool square = isSquareInPlace();
            return largeTriangles && mediumTriangle && parallelogram && smallTriangle && square;
        }



        #region Large Triangle Validation

        public bool areLargeTrianglesInPlace()
        {
            getLargeTriangleCorner();
            return
                checkLargeTrianglesToMapCorner()
                &&
                checkLargeTrianglesAgainstEachOther()
                &&
                checkLargeTrianglesNotOverlapping();
        }

        private bool checkLargeTrianglesToMapCorner()
        {
            PointF absA_1 = setOfTans[0].GetAbsolutePoint('A');
            foreach (PointF corner in mapCorners)
            {
                if (absA_1.IsCloseEnoughTo(corner))
                    return true;
            }

            PointF absB_1 = setOfTans[0].GetAbsolutePoint('B');
            foreach (PointF corner in mapCorners)
            {
                if (absA_1.IsCloseEnoughTo(corner))
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

        public int getLargeTriangleCorner()
        {
            //A&B OR B&A
            PointF absA_1 = setOfTans[0].GetAbsolutePoint('A');
            PointF absA_2 = setOfTans[1].GetAbsolutePoint('A');
            PointF absB_1 = setOfTans[0].GetAbsolutePoint('B');
            PointF absB_2 = setOfTans[1].GetAbsolutePoint('B');

            for (int i = 0; i < mapCorners.Length; i++)
            {
                if (absA_1.IsCloseEnoughTo(mapCorners[i]) && absB_2.IsCloseEnoughTo(mapCorners[i]))
                {
                    largeTriangleCornerNumber = i;
                }
                if (absB_1.IsCloseEnoughTo(mapCorners[i]) && absA_2.IsCloseEnoughTo(mapCorners[i]))
                {
                    largeTriangleCornerNumber = i;
                }
            }
            return largeTriangleCornerNumber;
        }

        #endregion


        #region Medium Triangle Validation

        private bool isMediumTriangleInPlace()
        {
            return
                checkMediumTriangleToOppositeMapCorner()
                &&
                checkMediumTriangleAngledRight();
        }

        private bool checkMediumTriangleToOppositeMapCorner()
        {
            PointF absC_2 = setOfTans[2].GetAbsolutePoint('C');
            oppositeCorner = (largeTriangleCornerNumber + 2) % 4;

            return absC_2.IsCloseEnoughTo(mapCorners[oppositeCorner]);
        }

        private bool checkMediumTriangleAngledRight()
        {
            switch (oppositeCorner)
            {
                case 0:
                    if (setOfTans[2].rotation == 135)
                        return true;
                    break;
                case 1:
                    if (setOfTans[2].rotation == 225)
                        return true;
                    break;
                case 2:
                    if (setOfTans[2].rotation == 315)
                        return true;
                    break;
                case 3:
                    if (setOfTans[2].rotation == 45)
                        return true;
                    break;
                default:
                    throw new Exception("No valid opposite corner was calculated.");
            }
            return false;
        }

        #endregion


        #region Parallelogram Validation

        private bool isParallelogramInPlace()
        {
            return checkParallelogramAgainstMediumTriangle();
        }

        private bool checkParallelogramAgainstMediumTriangle()
        {
            PointF absA_P = setOfTans[6].GetAbsolutePoint('A');
            PointF absC_P = setOfTans[6].GetAbsolutePoint('C');

            PointF absA_3 = setOfTans[2].GetAbsolutePoint('A');

            return (absA_P.IsCloseEnoughTo(absA_3) || absC_P.IsCloseEnoughTo(absA_3));
        }

        #endregion


        #region Small Triangle Validation

        private bool areSmallTrianglesInPlace()
        {
            return checkSmallTriangleAgainstParallelogram() &&
                   checkCenterSmallTriangleAgainstLargeTriangles() &&
                   checkSmallTriangleAgainstMiddleTriangle() &&
                   checkOutsideSmallTriangleAgainstLargeTriangles();
        }

        private bool checkCenterSmallTriangleAgainstLargeTriangles()
        {
            PointF absC_1 = setOfTans[0].GetAbsolutePoint('C');
            PointF absC_2 = setOfTans[1].GetAbsolutePoint('C');

            PointF absC_4 = setOfTans[3].GetAbsolutePoint('C');
            PointF absC_5 = setOfTans[4].GetAbsolutePoint('C');

            return (absC_4.IsCloseEnoughTo(absC_1) && absC_4.IsCloseEnoughTo(absC_2))
                || (absC_5.IsCloseEnoughTo(absC_1) && absC_5.IsCloseEnoughTo(absC_2));
        }

        private bool checkOutsideSmallTriangleAgainstLargeTriangles()
        {
            PointF absA_4 = setOfTans[3].GetAbsolutePoint('A');
            PointF absA_5 = setOfTans[4].GetAbsolutePoint('A');

            PointF absB_1 = setOfTans[0].GetAbsolutePoint('B');
            PointF absB_2 = setOfTans[1].GetAbsolutePoint('B');

            return absA_4.IsCloseEnoughTo(absB_1) || absA_4.IsCloseEnoughTo(absB_2) ||
                   absA_5.IsCloseEnoughTo(absB_1) || absA_5.IsCloseEnoughTo(absB_2);
        }

        private bool checkSmallTriangleAgainstParallelogram()
        {
            PointF absA_4 = setOfTans[3].GetAbsolutePoint('A');
            PointF absB_4 = setOfTans[3].GetAbsolutePoint('B');

            PointF absA_5 = setOfTans[4].GetAbsolutePoint('A');
            PointF absB_5 = setOfTans[4].GetAbsolutePoint('B');



            PointF absA_P = setOfTans[6].GetAbsolutePoint('A');
            PointF absB_P = setOfTans[6].GetAbsolutePoint('B');
            PointF absC_P = setOfTans[6].GetAbsolutePoint('C');
            PointF absD_P = setOfTans[6].GetAbsolutePoint('D');


            //checking two triangles and two parallelogram sides
            return (absA_4.IsCloseEnoughTo(absD_P) && absB_4.IsCloseEnoughTo(absA_P))
                                                   ||
                   (absA_5.IsCloseEnoughTo(absD_P) && absB_5.IsCloseEnoughTo(absA_P))
                                                   ||
                   (absA_4.IsCloseEnoughTo(absB_P) && absB_4.IsCloseEnoughTo(absC_P))
                                                   ||
                   (absA_5.IsCloseEnoughTo(absB_P) && absB_5.IsCloseEnoughTo(absC_P));
        }

        private bool checkSmallTriangleAgainstMiddleTriangle()
        {
            PointF absB_4 = setOfTans[3].GetAbsolutePoint('B');
            PointF absB_5 = setOfTans[4].GetAbsolutePoint('B');

            PointF absB_3 = setOfTans[2].GetAbsolutePoint('B');

            return absB_4.IsCloseEnoughTo(absB_3) || absB_5.IsCloseEnoughTo(absB_3);
        }

        #endregion


        #region Square Validation

        private bool isSquareInPlace()
        {
            PointF absA_S = setOfTans[5].GetAbsolutePoint('A');
            PointF absB_S = setOfTans[5].GetAbsolutePoint('B');
            PointF absC_S = setOfTans[5].GetAbsolutePoint('C');
            PointF absD_S = setOfTans[5].GetAbsolutePoint('D');

            PointF[] squareAbsPoints = new PointF[4]
            {
                absA_S,
                absB_S,
                absC_S,
                absD_S
            };

            return checkSquareAgainstLargeTriangles(squareAbsPoints) &&
                   checkSquareAgainstMediumTriangle(squareAbsPoints);
        }

        private bool checkSquareAgainstLargeTriangles(PointF[] squareAbsPoints)
        {

            PointF absC_1 = setOfTans[0].GetAbsolutePoint('C');
            PointF absC_2 = setOfTans[1].GetAbsolutePoint('C');

            foreach (PointF squarePoint in squareAbsPoints)
            {
                if (squarePoint.IsCloseEnoughTo(absC_1) && squarePoint.IsCloseEnoughTo(absC_2))
                {
                    return true;
                }
            }
            return false;

        }

        private bool checkSquareAgainstMediumTriangle(PointF[] squareAbsPoints)
        {
            PointF absC_3 = setOfTans[2].GetAbsolutePoint('B');

            foreach (PointF squarePoint in squareAbsPoints)
            {
                if (squarePoint.IsCloseEnoughTo(absC_3))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }

}

