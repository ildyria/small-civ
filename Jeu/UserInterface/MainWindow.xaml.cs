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

namespace UserInterface
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void quit_clicked(object sender, RoutedEventArgs e)
        {
            //MessageBoxImage.Warning is ugly ?
            if (MessageBox.Show("Êtes vous sur de vouloir quitter ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Close();
            } 
        }
        private void startLoadGame_clicked(object sender, RoutedEventArgs e)
        {

        }
        private void startNewGame_clicked(object sender, RoutedEventArgs e)
        {
            SmallWorld.UnitType[] listTribes = new SmallWorld.UnitType[2];
            SmallWorld.MapSize mapSize = new SmallWorld.MapSize();

            if (tribeJ1_dwarf.IsChecked == true)
            {
                listTribes[0] = SmallWorld.UnitType.DWARF;
            }
            else if (tribeJ1_elf.IsChecked == true)
            {
                listTribes[0] = SmallWorld.UnitType.ELF;
            }
            else if (tribeJ1_orc.IsChecked == true)
            {
                listTribes[0] = SmallWorld.UnitType.ORC;
            }
            else
            {
                //Error ?
            }

            if (tribeJ2_dwarf.IsChecked == true)
            {
                listTribes[1] = SmallWorld.UnitType.DWARF;
            }
            else if (tribeJ2_elf.IsChecked == true)
            {
                listTribes[1] = SmallWorld.UnitType.ELF;
            }
            else if (tribeJ2_orc.IsChecked == true)
            {
                listTribes[1] = SmallWorld.UnitType.ORC;
            }
            else
            {
                //Error ?
            }

            if (mapSize_demo.IsChecked == true)
            {
                mapSize = SmallWorld.MapSize.DEMO;
            }
            else if (mapSize_small.IsChecked == true)
            {
                mapSize = SmallWorld.MapSize.SMALL;
            }
            else if (mapSize_classic.IsChecked == true)
            {
                mapSize = SmallWorld.MapSize.CLASSIC;
            }
            else
            {
                //Error ?
            }

            SmallWorld.GameMakerNew gmn = new SmallWorld.GameMakerNew();
            gmn.setNames(new string[2] {p1Name.Text, p2Name.Text });
            gmn.setTribes(listTribes);
            gmn.setMapSize(mapSize);
            gmn.makeGame();

            // RENDER
        }
    }
}
