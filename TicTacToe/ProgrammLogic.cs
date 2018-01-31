using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    class ProgrammLogics
    {
        static bool isX = true;
        
        //This method adds to the square the correct symbol of the correct color
        public static void Multiplayer(Button curentZone)
        {
            if ((char)curentZone.Content != ' ')
                return;
            curentZone.Content = isX ? 'X' : 'O';
            curentZone.Foreground = isX ? Brushes.Red : Brushes.Blue;
            isX = !isX;
            ParseZone((char)curentZone.Content);
        }

        //Just clean table
        public static void CleanZone()
        {
            Array.ForEach(MainWindow.zones, l => l.Content = ' ');
            isX = true;
        }

        public static void ParseZone(char symb)
        {
            if (ParseWin(symb, BtnConverter()))
            {
                if (symb == 'X')
                    MainWindow.XWins++;
                else
                    MainWindow.OWins++;
                MessageBox.Show(symb + " Win!");
                CleanZone();
            }
            else if (ParseDraw(BtnConverter()))
            {
                MainWindow.Draw++;
                MessageBox.Show("Draw");
                CleanZone();
            }
        }

        public static bool ParseWin(char symb, char[] zones)
        {
            //I wrote this, because in this way I can reduce the winner search algorythm
            char[][] btnText = new char[][]
                {
                new char[] { zones[0], zones[1], zones[2] },
                new char[] { zones[3], zones[4], zones[5] },
                new char[] { zones[6], zones[7], zones[8] }
                };

            for (byte i = 0; i < btnText.GetLength(0); i++)
                if ((btnText[i][0] == symb && btnText[i][1] == symb && btnText[i][2] == symb) ||
                        (btnText[0][i] == symb && btnText[1][i] == symb && btnText[2][i] == symb) ||
                        (i == 0 && btnText[i][i] == symb && btnText[i + 1][i + 1] == symb && btnText[i + 2][i + 2] == symb) ||
                        (i == 2 && btnText[i][i - 2] == symb && btnText[i - 1][i - 1] == symb && btnText[i - 2][i] == symb))
                    return true;
            return false;
        }

        public static bool ParseDraw(char[] zones)
        {
            return Array.TrueForAll(zones, l => l != ' ');
        }

        public static void Surrender()
        {
            if (isX)
            {
                MainWindow.OWins++;
                MessageBox.Show("X Surrender!");
            }
            else
            {
                MainWindow.XWins++;
                MessageBox.Show("O Surrender!");
            }
            CleanZone();
        }

        public static char[] BtnConverter()
        {
            char[] charZones = new char[9];

            for (int i = 0; i < MainWindow.zones.Length; i++)
                charZones[i] = (char)MainWindow.zones[i].Content;

            return charZones;
        }
    }
}
