using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivy.Event
{
    public class AccelerometerEvent : IvyEvent
    {
        public static readonly int MAXX_VALUE = 100, MAXY_VALUE = 100, MAXZ_VALUE = 100,
            MINX_VALUE = -100, MINY_VALUE = 100, MINZ_VALUE = 100;

        private int x = 0;
        private int y = 0;
        private int z = 0;

        public AccelerometerEvent(int playerIdx, int eventX, int eventY, int eventZ)
            : base(playerIdx)
        {
            x = eventX;
            y = eventY;
            z = eventZ;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public int getZ()
        {
            return z;
        }

        public override String ToString()
        {
            StringBuilder str = new StringBuilder();

            str.Append("[ACCEL]");
            str.Append(" PLAYER=");
            str.Append(getPlayer());
            str.Append(", X=");
            str.Append(x);
            str.Append(", Y=");
            str.Append(y);
            str.Append(", Z=");
            str.Append(z);

            return str.ToString();
        }
    }
}
