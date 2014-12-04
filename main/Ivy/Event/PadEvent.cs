using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivy.Event
{
    public class PadEvent : IvyEvent
    {
        public enum PadButtonCommand
        {
            LEFT = 0,
            UP_LEFT = 1,
            UP = 2,
            UP_RIGHT = 3,
            RIGHT = 4,
            DOWN_RIGHT = 5,
            DOWN = 6,
            DOWN_LEFT = 7,
            T = 8,
            H = 9,
            I = 10,
            S = 11,
            SELECT = 12,
            PAUSE = 13,
            START = 14
        }

        private PadButtonAction state = PadButtonAction.DOWN_STATE;
        private PadButtonCommand button = PadButtonCommand.LEFT;
        private int x = -1;
        private int y = -1;


        public PadEvent(int playerIdx, PadButtonCommand key, PadButtonAction eventState)
            : base(playerIdx)
        {
            button = key;
            state = eventState;
        }

        public PadEvent(int playerIdx, PadButtonCommand key, PadButtonAction eventState, int eventX, int eventY)
            : this(playerIdx, key, eventState)
        {
            x = eventX;
            y = eventY;
        }

        public PadButtonCommand getSource()
        {
            return button;
        }

        public PadButtonAction getAction()
        {
            return state;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public static String padToString(PadButtonCommand code)
        {
            switch (code)
            {
                case PadButtonCommand.LEFT:
                    return "LEFT";
                case PadButtonCommand.UP_LEFT:
                    return "UP_LEFT";
                case PadButtonCommand.UP:
                    return "UP";
                case PadButtonCommand.UP_RIGHT:
                    return "UP_RIGHT";
                case PadButtonCommand.RIGHT:
                    return "RIGHT";
                case PadButtonCommand.DOWN_RIGHT:
                    return "DOWN_RIGHT";
                case PadButtonCommand.DOWN:
                    return "DOWN";
                case PadButtonCommand.DOWN_LEFT:
                    return "DOWN_LEFT";
                case PadButtonCommand.T:
                    return "T";
                case PadButtonCommand.H:
                    return "H";
                case PadButtonCommand.I:
                    return "I";
                case PadButtonCommand.S:
                    return "S";
                case PadButtonCommand.SELECT:
                    return "SELECT";
                case PadButtonCommand.PAUSE:
                    return "PAUSE";
                case PadButtonCommand.START:
                    return "START";

                default:
                    return "UNKNOWN";
            }
        }

        public override String ToString()
        {
            StringBuilder str = new StringBuilder();

            str.Append("[PAD]");
            str.Append(" PLAYER=");
            str.Append(getPlayer());
            str.Append(", BUTTON=");
            str.Append(padToString(button));
            str.Append(", STATE=");
            str.Append(stateToString(state));

            return str.ToString();
        }
    }
}
