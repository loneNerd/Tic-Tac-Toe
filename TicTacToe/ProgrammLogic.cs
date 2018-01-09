using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    class ProgrammLogic
    {
        static bool isX = true;

        public static void Singleplayer(Button button)
        {
            if (button.HasContent)
                return;
            if (isX == true)
            {
                button.Content = 'X';
                button.Foreground = Brushes.Red;
            }
            else
            {
                button.Content = 'O';
                button.Foreground = Brushes.Blue;
            }
            isX = !isX;
        }

        public static void NewGame(Button[] btns)
        {
            for(uint i = 0; i < btns.Length; i++)
            {
                btns[i].Content = null;
                btns[i].Background = Brushes.White;
            }
            isX = true;
        }
    }
}
