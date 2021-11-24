using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Tangram.Classes.Game
{
    class Position
    {
        PointF point;
        double rotation;

        public Position(PointF point, double rotation)
        {
            this.point = point;
            this.rotation = rotation;
        }
    }
}
