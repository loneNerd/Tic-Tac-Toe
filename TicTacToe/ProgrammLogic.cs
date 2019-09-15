using System;

namespace TicTacToe
{
    class ProgrammLogics
    {
        public static bool CheckDraw(char[] zones) => Array.TrueForAll(zones, zone => zone != ' ');

        public static int[] CheckWin(char[] zones, int currPosition)
        {
            if (currPosition < 0 || currPosition > 8 || zones[currPosition] == ' ')
                return null;

            switch(currPosition)
            {
                case 0:
                case 3:
                case 6:
                    if (zones[currPosition] == zones[currPosition + 1] && zones[currPosition + 1] == zones[currPosition + 2])
                        return new int[3] { currPosition, currPosition + 1, currPosition + 2 };
                    break;

                case 1:
                case 4:
                case 7:
                    if (zones[currPosition - 1] == zones[currPosition] && zones[currPosition] == zones[currPosition + 1])
                        return new int[3] { currPosition - 1, currPosition, currPosition + 1 };
                    break;

                case 2:
                case 5:
                case 8:
                    if (zones[currPosition - 2] == zones[currPosition - 1] && zones[currPosition - 1] == zones[currPosition])
                        return new int[3] { currPosition - 2, currPosition - 1, currPosition };
                    break;
            }

            switch (currPosition)
            {
                case 0:
                case 1:
                case 2:
                    if (zones[currPosition] == zones[currPosition + 3] && zones[currPosition + 3] == zones[currPosition + 6])
                        return new int[3] { currPosition, currPosition + 3, currPosition + 6 };
                    break;

                case 3:
                case 4:
                case 5:
                    if (zones[currPosition - 3] == zones[currPosition] && zones[currPosition] == zones[currPosition + 3])
                        return new int[3] { currPosition - 3, currPosition, currPosition + 3 };
                    break;

                case 6:
                case 7:
                case 8:
                    if (zones[currPosition - 6] == zones[currPosition - 3] && zones[currPosition - 3] == zones[currPosition])
                        return new int[3] { currPosition - 6, currPosition - 3, currPosition };
                    break;
            }

            if (zones[0] != ' ' && zones[0] == zones[4] && zones[4] == zones[8])
                return new int[3] { 0, 4, 8 };
            else if (zones[2] != ' ' && zones[2] == zones[4] && zones[4] == zones[6])
                return new int[3] { 2, 4, 6 };

            return null;
        }
    }
}
