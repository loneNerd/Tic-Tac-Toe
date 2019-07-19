using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool isSingleplayer = false;
        public static bool playerFirstMove = true;
        static int xWins = 0;
        static int oWins = 0;
        static int draw = 0;

        public static Button[] zones;
        //static Label xWinsLabel;
        //static Label oWinsLabel;
        //static Label drawLabel;

        //The next three properties I wrote just because
        //I'm too lazy to write WinsLabel =
        public static int XWins
        {
            get { return xWins; }
            set { xWins++; }//xWinsLabel.Content = "X Wins: " + value.ToString(); }
        }

        public static int OWins
        {
            get { return oWins; }
            set { oWins++; }// oWinsLabel.Content = "O Wins: " + value.ToString(); }
        }

        public static int Draw
        {
            get { return draw; }
            set { draw++; }//drawLabel.Content = "Draw: " + value.ToString(); }
        }

        public MainWindow()
        {
            InitializeComponent();
            zones = new Button[] { Zone1, Zone2, Zone3, Zone4, Zone5, Zone6, Zone7, Zone8, Zone9 };
            //xWinsLabel = XWinsLabel;
            //oWinsLabel = OWinsLabel;
            //drawLabel = DrawLabel;
        }

        private void ZoneClick(object sender, RoutedEventArgs e)
        {
            if ((char)((Button)sender).Content == ' ')
            {
                if (!isSingleplayer)
                    ProgrammLogics.Multiplayer((Button)sender);
                else
                {
                    ((Button)sender).Content = AI.player;
                    ((Button)sender).Foreground = AI.player == 'X' ? Brushes.Red : Brushes.Blue;
                    if (ProgrammLogics.ParseDraw(ProgrammLogics.BtnConverter()))
                    {
                        ProgrammLogics.ParseZone(AI.player);
                        return;
                    }
                    else
                        ProgrammLogics.ParseZone(AI.player);
                    AI.AITactics();
                    ProgrammLogics.ParseZone(AI.ai);
                }
            }
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        //Next two events and two methods just for style
        //And increase the inscription of the button
        private void GULeave(object sender, MouseEventArgs e)
        {
            ((Label)sender).Height = 50;
            ((Label)sender).Width = 150;
        }

        private void GUMove(object sender, MouseEventArgs e)
        {
            ((Label)sender).Height = 60;
            ((Label)sender).Width = 160;
        }

        private void ChangeButton(object sender, MouseEventArgs e)
        {
            ((Grid)sender).Background = Brushes.Gray;
        }

        private void RemoveButton(object sender, MouseEventArgs e)
        {
            ((Grid)sender).Background = Brushes.Transparent;
        }

        private void GameModeClick(object sender, MouseButtonEventArgs e)
        {
            if (((Grid)sender).Name == "SingleplayerButton")
            {
                isSingleplayer = true;
            }
            else
            {
                isSingleplayer = false;
                ProgrammLogics.CleanZone();
            }
            GameMode.Visibility = Visibility.Hidden;
        }

        private void ChoiceSide(object sender, MouseButtonEventArgs e)
        {
            /*if (sender.GetType() == OSymbol.GetType())
            {
                ProgrammLogics.isX = false;
                AI.ai = 'X';
                AI.player = 'O';
            }
            else if (sender.GetType() == XSymbol.GetType())
            {
                ProgrammLogics.isX = true;
                AI.ai = 'O';
                AI.player = 'X';
            }
            ProgrammLogics.CleanZone();
            YourChoice.Visibility = Visibility.Hidden;*/
        }

        private void AddShadow(object sender, MouseEventArgs e)
        {
            /*if (sender.GetType() == OSymbol.GetType())
                OShadow.Color = Colors.Cyan;
            else if (sender.GetType() == XSymbol.GetType())
                XShadow.Color = Colors.Pink;*/

        }

        private void RemoveShadow(object sender, MouseEventArgs e)
        {
            /*if (sender.GetType() == OSymbol.GetType())
                OShadow.Color = Colors.Blue;
            else if (sender.GetType() == XSymbol.GetType())
                XShadow.Color = Colors.Red;*/

        }

        private void Surrender(object sender, RoutedEventArgs e)
        {
            ProgrammLogics.Surrender();
        }

        private void MainScreen(object sender, RoutedEventArgs e)
        {
            GameMode.Visibility = Visibility.Visible;
            playerFirstMove = true;
            xWins = -1;
            oWins = -1;
            draw = -1;

            XWins = 0;
            OWins = 0;
            Draw = 0;
            ProgrammLogics.CleanZone();
        }

        private void WhoTurn(object sender, MouseButtonEventArgs e)
        {
            /*if (((Grid)sender).Name == "ComputerFirst")
                playerFirstMove = false;
            else
                playerFirstMove = true;

            YourChoice.Visibility = Visibility.Visible;*/
        }

        private void CloseProgramm(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimazeProgramm(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}