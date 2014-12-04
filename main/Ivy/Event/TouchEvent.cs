using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivy.Event
{
    public class TouchEvent : IvyEvent
    {
        private int x = -1;
        private int y = -1;
        private PadButtonAction state = PadButtonAction.DOWN_STATE;

        public TouchEvent(int playerIdx, int xTouch, int yTouch, int state) : base(playerIdx)
        {
            x = xTouch;
            y = yTouch;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public PadButtonAction getAction()
        {
            return state;
        }

        public override String ToString()
        {
            StringBuilder str = new StringBuilder();

            str.Append("[TOUCH]");
            str.Append(" PLAYER=");
            str.Append(getPlayer());
            str.Append(", X=");
            str.Append(x);
            str.Append(", Y=");
            str.Append(y);
            str.Append(", STATE=");
            str.Append(stateToString(state));

            return str.ToString();
        }
    }
}
