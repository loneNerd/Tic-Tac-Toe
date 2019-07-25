using System;

namespace TicTacToe
{
    class ProgrammLogics
    {
        public static bool CheckDraw(char[] zones) => Array.TrueForAll(zones, zone => zone != ' ');

        public static int[] CheckWin(char[] zones, char currSymb, int currPosition)
        {
            if (currPosition < 0 || currPosition > 8)
                return null;

            switch(currPosition)
            {
                case 0:
                case 3:
                case 6:
                    if (zones[currPosition] == currSymb && zones[currPosition + 1] == currSymb && zones[currPosition + 2] == currSymb)
                        return new int[3] { currPosition, currPosition + 1, currPosition + 2 };
                    break;

                case 1:
                case 4:
                case 7:
                    if (zones[currPosition - 1] == currSymb && zones[currPosition] == currSymb && zones[currPosition + 1] == currSymb)
                        return new int[3] { currPosition - 1, currPosition, currPosition + 1 };
                    break;

                case 2:
                case 5:
                case 8:
                    if (zones[currPosition - 2] == currSymb && zones[currPosition - 1] == currSymb && zones[currPosition] == currSymb)
                        return new int[3] { currPosition - 2, currPosition - 1, currPosition };
                    break;
            }

            switch (currPosition)
            {
                case 0:
                case 1:
                case 2:
                    if (zones[currPosition] == currSymb && zones[currPosition + 3] == currSymb && zones[currPosition + 6] == currSymb)
                        return new int[3] { currPosition, currPosition + 3, currPosition + 6 };
                    break;

                case 3:
                case 4:
                case 5:
                    if (zones[currPosition - 3] == currSymb && zones[currPosition] == currSymb && zones[currPosition + 3] == currSymb)
                        return new int[3] { currPosition - 3, currPosition, currPosition + 3 };
                    break;

                case 6:
                case 7:
                case 8:
                    if (zones[currPosition - 6] == currSymb && zones[currPosition - 3] == currSymb && zones[currPosition] == currSymb)
                        return new int[3] { currPosition - 6, currPosition - 3, currPosition };
                    break;
            }

            if (zones[0] == currSymb && zones[4] == currSymb && zones[8] == currSymb)
                return new int[3] { 0, 4, 8 };
            else if (zones[2] == currSymb && zones[4] == currSymb && zones[6] == currSymb)
                return new int[3] { 2, 4, 6 };

            return null;
        }
    }
}
