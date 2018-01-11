using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    class ProgrammLogic
    {
        static bool isX = true;

        public static void Singleplayer(Button curentZone)
        {
            if ((char)curentZone.Content != ' ')
                return;
            if (isX == true)
            {
                isX = !isX;
                curentZone.Content = 'X';
                curentZone.Foreground = Brushes.Red;
                ParseZone('X');
            }
            else
            {
                isX = !isX;
                curentZone.Content = 'O';
                curentZone.Foreground = Brushes.Blue;
                ParseZone('O');
            }
        }

        public static void CleanZone()
        {
            for(ushort i = 0; i < 3; i++)
            {
                for (ushort j = 0; j < 3; j++)
                {
                    MainWindow.zones[i][j].Content = ' ';
                    MainWindow.zones[i][j].Background = Brushes.White;
                }
            }
            isX = true;
        }

        public static void ParseZone(char symb)
        {
            char[][] btnText = new char[][]
            {
                new char[] { (char)MainWindow.zones[0][0].Content, (char)MainWindow.zones[0][1].Content, (char)MainWindow.zones[0][2].Content },
                new char[] { (char)MainWindow.zones[1][0].Content, (char)MainWindow.zones[1][1].Content, (char)MainWindow.zones[1][2].Content },
                new char[] { (char)MainWindow.zones[2][0].Content, (char)MainWindow.zones[2][1].Content, (char)MainWindow.zones[2][2].Content }

            };

            for (byte i = 0; i < btnText.GetLength(0); i++)
            {
                if ((btnText[i][0] == symb && btnText[i][1] == symb && btnText[i][2] == symb) |
                    (btnText[0][i] == symb && btnText[1][i] == symb && btnText[2][i] == symb) |
                    (i == 0 && btnText[i][i] == symb && btnText[i + 1][i + 1] == symb && btnText[i + 2][i + 2] == symb) |
                    (i == 2 && btnText[i][i - 2] == symb && btnText[i - 1][i - 1] == symb && btnText[i - 2][i] == symb))
                {
                    MessageBox.Show(symb + " Win!");
                    CleanZone();
                    return;
                }
            }
            if (btnText[0][0] != ' ' && btnText[0][1] != ' ' && btnText[0][2] != ' ' &&
                btnText[1][0] != ' ' && btnText[1][1] != ' ' && btnText[1][2] != ' ' &&
                btnText[2][0] != ' ' && btnText[2][1] != ' ' && btnText[2][2] != ' ')
            {
                MessageBox.Show("Draw");
                CleanZone();
            }
        }

        public static void GiveUp()
        {
            if (isX)
            {
                MessageBox.Show("X Give Up!");
            }
            else
            {
                MessageBox.Show("O Give Up!");
            }
            CleanZone();
        }
    }
}
