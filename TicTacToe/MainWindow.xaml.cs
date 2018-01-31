using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isSingleplayer = false;
        static uint xWins = 0;
        static uint oWins = 0;
        static uint draw = 0;

        public static Button[] zones;
        static Label xWinsLabel;
        static Label oWinsLabel;
        static Label drawLabel;

        //The next three properties I wrote just because
        //I'm too lazy to write WinsLabel =
        public static uint XWins
        {
            get { return xWins; }
            set { xWins++;  xWinsLabel.Content = "X Wins: " + value.ToString(); }
        }

        public static uint OWins
        {
            get { return oWins; }
            set { oWins++;  oWinsLabel.Content = "O Wins: " + value.ToString(); }
        }

        public static uint Draw
        {
            get { return draw; }
            set { draw++;  drawLabel.Content = "Draw: " + value.ToString(); }
        }

        public MainWindow()
        {
            InitializeComponent();
            zones = new Button[] { zone_1, zone_2, zone_3, zone_4, zone_5, zone_6, zone_7, zone_8, zone_9 };
            xWinsLabel = XWinsLabel;
            oWinsLabel = OWinsLabel;
            drawLabel = DrawLabel;
            ProgrammLogics.CleanZone();
        }
        
        private void ZoneClick(object sender, RoutedEventArgs e)
        {
            if ((char)((Button)sender).Content == ' ')
            {
                if (isSingleplayer)
                {
                    ProgrammLogics.Multiplayer((Button)sender);
                }
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
        
        //Right name for this events "Surrender"
        //I change this event twice and I'll not be any more
        private void GiveUp(object sender, MouseButtonEventArgs e)
        {
            ProgrammLogics.Surrender();
        }

        //Next two events and two methods just for style
        //And increase the inscription of the button
        private void GULeave(object sender, MouseEventArgs e)
        {
            ReduceText(ref GUEllipse, ref GULabel);
        }

        private void GUMove(object sender, MouseEventArgs e)
        {
            EnlargeText(ref GUEllipse, ref GULabel);
        }

        void EnlargeText(ref Ellipse elps, ref Label lbl)
        {
            lbl.FontSize = 30;
            elps.Height = 50;
            elps.Width = 150;
        }

        void ReduceText(ref Ellipse elps, ref Label lbl)
        {
            lbl.FontSize = 25;
            elps.Height = 45;
            elps.Width = 145;
        }
    }
}
