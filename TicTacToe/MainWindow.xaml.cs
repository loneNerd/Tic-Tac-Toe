using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isSingleplayer = true;

        Button[] zones;

        public MainWindow()
        {
            InitializeComponent();
            zones = new Button[] { zone_1, zone_2, zone_3, zone_4, zone_5, zone_6, zone_7, zone_8, zone_9 };
        }
        
        private void ZoneClick(object sender, RoutedEventArgs e)
        {
            if (isSingleplayer)
                ProgrammLogic.Singleplayer((Button)sender);
        }

        private void NewGameClick(object sender, MouseButtonEventArgs e)
        {
            ProgrammLogic.NewGame(zones);
        }

        private void GULeave(object sender, MouseEventArgs e)
        {
            ReduceText(GUEllipse, GULabel);
        }

        private void GUMove(object sender, MouseEventArgs e)
        {
            EnlargeText(GUEllipse, GULabel);
        }

        void EnlargeText(Ellipse elps, Label lbl)
        {
            lbl.FontSize = 30;
            elps.Height = 50;
            elps.Width = 150;
        }

        void ReduceText(Ellipse elps, Label lbl)
        {
            lbl.FontSize = 25;
            elps.Height = 45;
            elps.Width = 145;
        }
    }
}
