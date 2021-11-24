using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Tangram.Classes.PointExtensions
{
    public static class PointRotationExtension
    {
        public static PointF RotatePoint(this PointF pointToRotate, PointF center, float angleInDegrees)
        {
            float angleInRadians = angleInDegrees * (float)Math.PI / 180;
            //float wasn't enough accurate in this case, caused distortion
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);


            return new PointF
            {
                X = (float)
                (cosTheta * (pointToRotate.X - center.X) -
                sinTheta * (pointToRotate.Y - center.Y) + center.X),

                Y = (float)
                (sinTheta * (pointToRotate.X - center.X) +
                cosTheta * (pointToRotate.Y - center.Y) + center.Y)
            };
        }

        public static bool IsCloseEnoughTo(this Point thisPoint, Point thatPoint, int tolerance = 2)
        {
            int xDiff = thisPoint.X - thatPoint.X;
            int yDiff = thisPoint.Y - thatPoint.Y;

            return xDiff < tolerance && yDiff < tolerance;
        }

        public static bool IsCloseEnoughTo(this PointF thisPoint, PointF thatPoint, int tolerance = 2)
        {
            double xDiff = thisPoint.X - thatPoint.X;
            double yDiff = thisPoint.Y - thatPoint.Y;

            return Math.Abs(xDiff) < tolerance && Math.Abs(yDiff) < tolerance;
        }
    }
}
