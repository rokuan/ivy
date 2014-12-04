using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivy.Event
{
    public class IvyEvent
    {
        public enum PadButtonAction
        {
            DOWN_STATE = 0,
            UP_STATE = 1,
            MOVE_STATE = 2,
        }

        private int playerIndex = -1;

        protected IvyEvent(int playerIdx)
        {
            playerIndex = playerIdx;
        }

        public int getPlayer()
        {
            return playerIndex;
        }

        public static string stateToString(PadButtonAction stateMode)
        {
            switch (stateMode)
            {
                case PadButtonAction.DOWN_STATE:
                    return "DOWN";

                case PadButtonAction.UP_STATE:
                    return "UP";

                case PadButtonAction.MOVE_STATE:
                    return "MOVE";

                default:
                    return "UNKNOWN";
            }
        }
    }
}
