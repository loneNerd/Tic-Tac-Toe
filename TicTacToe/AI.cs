using System.Collections.Generic;

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

        private static ZonePoints CheckZonePoints(char[] playField, char symbol, int depthLevel = 0)
        {
            ZonePoints[] posibleMoves = GetEmptyIndexes(playField);
            ZonePoints? bestZone = null;
            depthLevel++;

            foreach (ZonePoints zone in posibleMoves)
            {
                char[] newBoard = (char[])playField.Clone();
                newBoard[zone.Zone] = symbol;

                ZonePoints newZone = zone;

                if (ProgrammLogics.CheckWin(newBoard, zone.Zone) != null && symbol == MainWindow.Player2Symbol)
                    newZone.Points = depthLevel;
                else if (ProgrammLogics.CheckWin(newBoard, zone.Zone) != null && symbol == MainWindow.Player1Symbol)
                    newZone.Points = -depthLevel;
                else if (!ProgrammLogics.CheckDraw(newBoard))
                    newZone.Points = CheckZonePoints(newBoard, symbol == 'X' ? 'O' : 'X', depthLevel).Points;

                if (bestZone == null ||
                    (symbol == MainWindow.Player2Symbol && newZone.Points > ((ZonePoints)bestZone).Points) ||
                    (symbol == MainWindow.Player1Symbol && newZone.Points < ((ZonePoints)bestZone).Points))
                    bestZone = newZone;
            }

            return (ZonePoints)bestZone;
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
