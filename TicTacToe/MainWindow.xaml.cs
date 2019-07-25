using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        public bool IsSingleplayer     { get; private set; }
        public bool IsPlayer1FirstMove { get; private set; }
        public char Player1Symbol      { get; private set; }
        public char Player2Symbol      { get; private set; }
   
        public char[] Zones { get { return Array.ConvertAll(zonesBttn, zone => (char)zone.Content); } }

        private Button[] zonesBttn;

        private int player1Wins  = 0;
        private int player2Wins  = 0;
        private int computerWins = 0;
        private int draws        = 0;

        private char currentSymbol;

        public MainWindow()
        {
            InitializeComponent();

            Instance = this;

            zonesBttn = new Button[] { Zone1, Zone2, Zone3, Zone4, Zone5, Zone6, Zone7, Zone8, Zone9 };
        }

        private void IncrementPlayer1Wins()
        {
            ++player1Wins;

            if (IsSingleplayer)
                PlayerWinsTB.Text = "Player Wins: " + player1Wins;
            else
                PlayerWinsTB.Text = "Player 1 Wins: " + player1Wins;
        }

        private void IncrementPlayer2Wins()
        {
            ++player2Wins;
            ComputerWinsTB.Text = "Player 2 Wins: " + player2Wins;
        }

        private void IncrementComputerWins()
        {
            ++computerWins;
            ComputerWinsTB.Text = "Computer Wins: " + computerWins;
        }

        private void IncrementDraws()
        {
            ++draws;
            DrawsTB.Text = "Draws: " + draws;
        }

        public void ClearPlayField() { Array.ForEach(zonesBttn, zone => zone.Content = ' '); }

        private void MinimazeProgramm(object sender, RoutedEventArgs e) { WindowState = WindowState.Minimized; }
        private void CloseProgramm(object sender, RoutedEventArgs e) { Close(); }
        private void MoveWindow(object sender, MouseButtonEventArgs e) { DragMove(); }

        private void SetGameMode(object sender, RoutedEventArgs e)
        {
            ClearPlayField();
            GameMode.Visibility = Visibility.Hidden;
            FirstMove.Visibility = Visibility.Visible;

            if (((Button)sender).Name == SingleplayerButton.Name)
            {
                IsSingleplayer = true;
                PlayerFirstMoveButton.Content = "Player First";
                PlayerFirstMoveButton.Tag = "Icons/Singleplayer.png";
                ComputerFirstMoveButton.Content = "Computer First";
                ComputerFirstMoveButton.Tag = "Icons/Computer.png";

                if (!IsSingleplayer)
                {
                    player1Wins = -1;
                    computerWins = -1;
                    IncrementPlayer1Wins();
                    IncrementComputerWins();
                }
                else
                    ComputerWinsTB.Text = "Computer Wins: " + computerWins;
            }
            else
            {
                IsSingleplayer = false;
                PlayerFirstMoveButton.Content = "Player 1 First";
                PlayerFirstMoveButton.Tag = "Icons/Singleplayer.png";
                ComputerFirstMoveButton.Content = "Player 2 First";
                ComputerFirstMoveButton.Tag = "Icons/Singleplayer.png";

                if (IsSingleplayer)
                {
                    player1Wins = -1;
                    player2Wins = -1;
                    IncrementPlayer1Wins();
                    IncrementPlayer2Wins();
                }
                else
                    ComputerWinsTB.Text = "Player 2 Wins: " + player2Wins;
            }
        }

        private void FirstTurn(object sender, RoutedEventArgs e)
        {
            FirstMove.Visibility = Visibility.Hidden;
            PickFigure.Visibility = Visibility.Visible;

            if (((Button)sender).Name == PlayerFirstMoveButton.Name)
                IsPlayer1FirstMove = true;
            else
                IsPlayer1FirstMove = false;
        }

        private void GetFigure(object sender, RoutedEventArgs e)
        {
            PickFigure.Visibility = Visibility.Hidden;
            PlayZone.Visibility = Visibility.Visible;
            currentSymbol = ((string)((Button)sender).Content)[0];

            Player1Symbol = currentSymbol;
            Player2Symbol = currentSymbol == 'X' ? 'O' : 'X';
            PlayerWinsTB.Foreground = currentSymbol == 'X' ? Brushes.Red : Brushes.Blue;
            ComputerWinsTB.Foreground = currentSymbol == 'X' ? Brushes.Blue : Brushes.Red;

            if (!IsPlayer1FirstMove)
                currentSymbol = Player1Symbol == 'X' ? 'O' : 'X';
        }

        private void ZoneClick(object sender, RoutedEventArgs e)
        {
            if ((char)((Button)sender).Content != ' ')
                return;

            ((Button)sender).Content = currentSymbol;
            ((Button)sender).Foreground = currentSymbol == 'X' ? Brushes.Red : Brushes.Blue;

            int[] winPos = ProgrammLogics.CheckWin(Zones, currentSymbol, Array.FindIndex(zonesBttn, zone => zone.Name == ((Button)sender).Name));

            if (ProgrammLogics.CheckDraw(Zones))
            {
                MessageBox.Show("Draw!", "Game Over");
                ClearPlayField();
                IncrementDraws();

                if (!IsSingleplayer)
                    currentSymbol = IsPlayer1FirstMove ? Player2Symbol : Player1Symbol;
            }
            else if (winPos != null)
            {
                Brush prevColor = zonesBttn[0].Background;

                foreach (int w in winPos)
                    zonesBttn[w].Background = Brushes.Green;

                if (currentSymbol == Player1Symbol)
                {
                    if (IsSingleplayer)
                        MessageBox.Show("You win!", "Game Over");
                    else
                        MessageBox.Show("Player 1 Win!", "Game Over");
                    IncrementPlayer1Wins();
                }
                else
                {
                    MessageBox.Show("Player 2 Win!", "Game Over");
                    IncrementPlayer2Wins();
                }

                foreach (int w in winPos)
                    zonesBttn[w].Background = prevColor;

                ClearPlayField();

                if (!IsSingleplayer)
                    currentSymbol = IsPlayer1FirstMove ? Player2Symbol : Player1Symbol;
            }
            
            if (IsSingleplayer)
            {
                zonesBttn[AI.AIMove()].Content = Player2Symbol;
                winPos = ProgrammLogics.CheckWin(Zones, currentSymbol, Array.FindIndex(zonesBttn, zone => zone.Name == ((Button)sender).Name));

                if (winPos != null)
                {
                    Brush prevColor = zonesBttn[0].Background;

                    foreach (int w in winPos)
                        zonesBttn[w].Background = Brushes.Green;

                    MessageBox.Show("Computer Win!", "Game Over");
                    IncrementComputerWins();

                    foreach (int w in winPos)
                        zonesBttn[w].Background = prevColor;

                    ClearPlayField();
                    currentSymbol = IsPlayer1FirstMove ? Player1Symbol : Player2Symbol;
                    return;
                }
                else if (ProgrammLogics.CheckDraw(Zones))
                {
                    MessageBox.Show("Draw!", "Game Over");
                    ClearPlayField();
                    IncrementDraws();
                }
            }
            else
                currentSymbol = currentSymbol == 'X' ? 'O' : 'X';
        }

        private void GiveUp(object sender, RoutedEventArgs e)
        {
            ClearPlayField();

            if (IsSingleplayer)
            {
                IncrementComputerWins();
                MessageBox.Show("Computer Win!", "Game Over");
            }
            else
            {
                if (currentSymbol == Player1Symbol)
                {
                    IncrementPlayer2Wins();
                    MessageBox.Show("Player 2 Win!", "Game Over");
                }
                else
                {
                    IncrementPlayer1Wins();
                    MessageBox.Show("Player 1 Win!", "Game Over");
                }
            }

            currentSymbol = IsPlayer1FirstMove ? Player1Symbol : Player2Symbol;
        }

        private void MainScreen(object sender, RoutedEventArgs e)
        {
            GameMode.Visibility = Visibility.Visible;
            PlayZone.Visibility = Visibility.Hidden;
            ClearPlayField();
        }

    }
}