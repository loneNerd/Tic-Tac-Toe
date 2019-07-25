using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Controls;

namespace TicTacToe
{
    class AI
    {
        public static int AIMove()
        {
            return 0;
        }
        static int depthLevel = 0;

        static Zone[] EmptyIndex(char[] zones)
        {
            List<Zone> emptyIndex = new List<Zone>();

            for (int i = 0; i < zones.Length; i++)
                if (zones[i] == ' ')
                    emptyIndex.Add(new Zone(i));

            return emptyIndex.ToArray();
        }

        public static void AITactics()
        {
            Button AIMove = MainWindow.zonesBttn[BestZone(ProgrammLogics.BtnConverter(), ai).position];
            depthLevel = 0;
            AIMove.Content = ai;
            AIMove.Foreground = ai == 'X' ? Brushes.Red : Brushes.Blue;
        }

        public static Zone BestZone(char[] curentBoard, char plr)
        {
            Zone[] freeZone = EmptyIndex(curentBoard);
            char[] newBoard;

            Zone? bestZone = null;

            depthLevel++;

            foreach(Zone zone in freeZone)
            {
                newBoard = (char[])curentBoard.Clone();
                newBoard[zone.position] = plr;

                Zone newZone = zone;
                
                if (ProgrammLogics.ParseWin(ai, newBoard))
                    newZone.rank += depthLevel;
                else if (ProgrammLogics.ParseWin(player, newBoard))
                    newZone.rank -= depthLevel;
                else if (!ProgrammLogics.CheckDraw(newBoard))
                {
                    if (plr == player)
                        newZone.rank = BestZone(newBoard, ai).rank;
                    else
                        newZone.rank = BestZone(newBoard, player).rank;
                }

                if (bestZone == null ||
                    (plr == ai && newZone.rank > ((Zone)bestZone).rank) ||
                    (plr == player && newZone.rank < ((Zone)bestZone).rank))
                    bestZone = newZone;
            }

            return (Zone)bestZone;
        }

        public struct Zone
        {
            public int position;
            public int rank;

            public Zone(int pos)
            {
                this.position = pos;
                rank = 0;
            }
        }
    }
}
