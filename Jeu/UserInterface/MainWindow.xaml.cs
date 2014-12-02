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
using System.Drawing;

namespace UserInterface
{

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SmallWorld.GameManager _gManager;
        //private Polygon _playerCursor;
        private int _iSelected;
        private int _jSelected;
        private List<SmallWorld.Unit> _unitsOnTile;
        private int _currentUnitNumber;
        
        public MainWindow()
        {
            InitializeComponent();
            //_playerCursor = (Polygon)this.Resources["cursorHex"];
            _unitsOnTile = new List<SmallWorld.Unit>();
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
            SmallWorld.GameMakerLoad gmn = new SmallWorld.GameMakerLoad();
            _gManager = gmn.makeGame();
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
            _gManager = gmn.makeGame();

            // RENDER
            //gameView.Visibility = Visibility.Visible;
            //createGameMenu.Visibility = Visibility.Collapsed;
            
        }
        private void mouseDown(object sender, MouseButtonEventArgs e)
        {
            if (mapView.IsMouseOver)
            {
                double ch = 2 * BoardView.TILESIZE / (Math.Sqrt(7) + 1);
                double a = (BoardView.TILESIZE - ch) / 2;
                System.Windows.Point p = e.GetPosition(mapCanvas);
                double x = p.X;
                double y = p.Y;
                int i, j;

                j = (int) (y / (BoardView.TILESIZE - a));
                double xOffset = (j % 2 == 1) ? BoardView.TILESIZE / 2 : 0;
                i = (int) ((x - xOffset) / BoardView.TILESIZE);

                //System.Diagnostics.Trace.WriteLine(x + " " + y);
                // no move if not on grid
                if (i < _gManager.getMap().getSize().Item1 && j < _gManager.getMap().getSize().Item2)
                {
                    _iSelected = i;
                    _jSelected = j;
                    double xR = xOffset + _iSelected * BoardView.TILESIZE;
                    double yR = _jSelected * (BoardView.TILESIZE - a);
                    // if it is not on the map, maybe make the cursor invisible ?
                    //playerCursor.Visibility = Visibility.Visible;
                    Canvas.SetLeft(playerCursor, xR);
                    Canvas.SetTop(playerCursor, yR);
                    selectedTileChanged();
                }
                
            }
        }
        private void selectedTileChanged()
        {
            _unitsOnTile = _gManager.getUnits().FindAll(u => u.getX() == _iSelected && u.getY() == _jSelected);
            _currentUnitNumber = 0;
            setEnableUnitsButtons();
            fillUnitInfo();
        }
        private void fillUnitInfo()
        {
           // buttons are disabled, so no problem should arise ...
           if (_unitsOnTile.Count != 0)
           {
               currentUnitListPosition.Content = _currentUnitNumber + "/" + _unitsOnTile.Count;
               SmallWorld.Unit unitToDetail = _unitsOnTile[_currentUnitNumber];
               name.Content = unitToDetail.getName();
               life.Content = unitToDetail.getLife();
               movesLeft.Content = unitToDetail.getMovesLeft();
           }
           else // resetting fields
           {
               currentUnitListPosition.Content = "";
               name.Content = "";
               life.Content = "";
               movesLeft.Content = "";
           }
        }

        //private void fillN
        private void fillGeneralInfo()
        {
            pointsJ1.Content = _gManager.getPlayer(1).getPoints() + " points";
            pointsJ2.Content = _gManager.getPlayer(2).getPoints() + " points";
            turn.Content = _gManager.getTurnCurrent() + "/" + _gManager.getTurnNumber();
            playerTurn.Content = _gManager.getPlayerTurn();
        }

        private void setEnableUnitsButtons()
        {
            if (_currentUnitNumber == 0)
            {
                buttonPrecUnit.IsEnabled = false;
            }
            else
            {
                buttonPrecUnit.IsEnabled = true;
            }

            if (_currentUnitNumber == _unitsOnTile.Count - 1 || _unitsOnTile.Count == 0)
            {
                buttonNextUnit.IsEnabled = false;
            }
            else
            {
                buttonNextUnit.IsEnabled = true;
            }
        }

        private void nextUnit_clicked(object sender, RoutedEventArgs e)
        {
            _currentUnitNumber++;
            fillUnitInfo();
        }
        private void precUnit_clicked(object sender, RoutedEventArgs e)
        {
            _currentUnitNumber--;
            fillUnitInfo();
        }
    }
}
