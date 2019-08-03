using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace TicTacToe
{
    class AI
    {
        private static ZonePoints[] GetEmptyIndexes(char[] zones)
        {
            List<ZonePoints> indexes = new List<ZonePoints>();

            for (int i = 0; i < zones.Length; ++i)
            {
                if (zones[i] == ' ')
                    indexes.Add(new ZonePoints(i));
            }

            return indexes.ToArray();
        }

        private static ZonePoints CheckZonePoints(char[] playField, char symbol, int depth = 0)
        {
            ZonePoints[] posibleMoves = GetEmptyIndexes(playField);
            char[] newPlayField = new char[playField.Length];
            ZonePoints bestMove = new ZonePoints(posibleMoves[0].Zone);

            for (int i = 0; i < posibleMoves.Length; ++i)
            {
                newPlayField = (char[])playField.Clone();
                newPlayField[posibleMoves[i].Zone] = symbol;

                if (ProgrammLogics.CheckWin(newPlayField, symbol, posibleMoves[i].Zone) != null)
                {
                    posibleMoves[i].Points = 10 - depth;
                    return posibleMoves[i];
                }
                else if (!ProgrammLogics.CheckDraw(newPlayField))
                {
                    ZonePoints tempZone = CheckZonePoints(newPlayField, symbol == 'X' ? 'O' : 'X', depth + 1);

                    if (tempZone.Points > bestMove.Points)
                        bestMove = tempZone;
                }
            }

            return bestMove;
        }

        public static int AIMove() => CheckZonePoints(MainWindow.Zones(), MainWindow.Player2Symbol).Zone;

        struct ZonePoints
        {
            public int Zone { get; set; }
            public int Points { get; set; }

            public ZonePoints(int zone)
            {
                Zone = zone;
                Points = 0;
            }
        }
    }
}
