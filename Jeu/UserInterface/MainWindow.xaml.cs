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
using SmallWorld;

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
        private Dictionary<SmallWorld.Unit, UIElement>  _visualUnitsElements;
        private static Dictionary<Type, string> typeStyle = new Dictionary<Type, string>(){
            {typeof(SmallWorld.Elf), "elf_unit"},
            {typeof(SmallWorld.Orc), "orc_unit"},
            {typeof(SmallWorld.Dwarf), "elf_unit"} // SHOULD BE CHANGED !
        };

        
        public MainWindow()
        {
            InitializeComponent();
            //_playerCursor = (Polygon)this.Resources["cursorHex"];
            _unitsOnTile = new List<SmallWorld.Unit>();
            _visualUnitsElements = new Dictionary<SmallWorld.Unit,UIElement>();
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
            //gmn.setNbUnit(new int)
            _gManager = gmn.makeGame();
            // RENDER
            //gameView.Visibility = Visibility.Visible;
            //createGameMenu.Visibility = Visibility.Collapsed;
            setUnitsOnMap();
            //Should start game instead
            _gManager.getPlayer(1).play();
            fillGeneralInfo();
            
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
               currentUnitListPosition.Content = (_currentUnitNumber+1) + "/" + _unitsOnTile.Count;
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
            playerTurn.Content = "Joueur " + (_gManager.getPlayerTurn() + 1);
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
            setEnableUnitsButtons();
            fillUnitInfo();
        }
        private void precUnit_clicked(object sender, RoutedEventArgs e)
        {
            _currentUnitNumber--;
            setEnableUnitsButtons();
            fillUnitInfo();
        }
        private void setUnitsOnMap()
        {
            foreach (SmallWorld.Unit u in _gManager.getUnits())
            {
                Label asset = new Label();
                asset.Style = App.Current.FindResource(typeStyle[u.GetType()]) as Style;
                
                //to convert to true coord
                Tuple<double, double> pos = indexToCoord(u.getX(), u.getY());
                Canvas.SetLeft(asset, pos.Item1);
                Canvas.SetTop(asset, pos.Item2);
                //mapControl.Children.Insert(2, rect);
                //Canvas.SetZIndex(rect, 2);
                mapControl.Children.Add(asset);
                _visualUnitsElements.Add(u, asset);

            }
        }

        private SmallWorld.Unit getCurrentUnit()
        {
            if (_unitsOnTile.Count != 0)
            {
                return _unitsOnTile[_currentUnitNumber];
            }
            return null;
        }

        private Tuple<double, double> indexToCoord(int i, int j)
        {
            double ch = 2 * BoardView.TILESIZE / (Math.Sqrt(7) + 1);
            double a = (BoardView.TILESIZE - ch) / 2;
            double xOffset = getXOffset(j);

            double xR = xOffset + i* BoardView.TILESIZE;
            double yR = j * (BoardView.TILESIZE - a);
            return new Tuple<double, double>(xR, yR);
        }

        private Tuple<int, int> coordToIndex(double x, double y)
        {
            double ch = 2 * BoardView.TILESIZE / (Math.Sqrt(7) + 1);
            double a = (BoardView.TILESIZE - ch) / 2;

            int j = (int)(y / (BoardView.TILESIZE - a));
            double xOffset = getXOffset(j);
            int i = (int)((x - xOffset) / BoardView.TILESIZE);
            return new Tuple<int, int>(i, j);
        }

        private int getXOffset(int j)
        {
            return (j % 2 == 1) ? BoardView.TILESIZE / 2 : 0;
        }

        private void moveUnit(int i, int j)
        {
            if (_unitsOnTile.Count != 0)
            {
                SmallWorld.Unit currentUnit = _unitsOnTile[_currentUnitNumber];
                _gManager.moveUnit(currentUnit, i, j);

                if (_gManager.getUnits().Count != _visualUnitsElements.Count) // not optimized at all
                {
                    List<SmallWorld.Unit> toDel = _visualUnitsElements.Keys.ToList().FindAll(u => !_gManager.getUnits().Contains(u));
                    foreach (SmallWorld.Unit u in toDel)
                    {
                        mapControl.Children.Remove(_visualUnitsElements[currentUnit]);
                        _visualUnitsElements.Remove(currentUnit);
                    }
                }
                if (currentUnit.getLife() == 0)
                {
                    //already done earlier in the ugly method above
                    //mapControl.Children.Remove(_visualUnitsElements[currentUnit]);
                    //_visualUnitsElements.Remove(currentUnit);
                }
                else 
                {
                    Tuple<double, double> coord = indexToCoord(currentUnit.getX(), currentUnit.getY());
                    Canvas.SetLeft(_visualUnitsElements[currentUnit], coord.Item1);
                    Canvas.SetTop(_visualUnitsElements[currentUnit], coord.Item2);
                }
            }  
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mapView.IsMouseOver)
            {
                if (_unitsOnTile.Count != 0) // checked twice, for compat
                {
                    System.Windows.Point p = e.GetPosition(mapView);
                    Tuple<int, int> coord = coordToIndex(p.X, p.Y);
                    moveUnit(coord.Item1, coord.Item2);
                    gameEnd();
                }
            }
        }

        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mapView.IsMouseOver)
            {
                System.Windows.Point p = e.GetPosition(mapPanel);
                Tuple<int, int> posRel = coordToIndex(p.X, p.Y);

                //System.Diagnostics.Trace.WriteLine(x + " " + y);
                // no move if not on grid
                if (posRel.Item1 < _gManager.getMap().getSize().Item1 && posRel.Item2 < _gManager.getMap().getSize().Item2)
                {
                    _iSelected = posRel.Item1;
                    _jSelected = posRel.Item2;
                    Tuple<double, double> coord = indexToCoord(_iSelected, _jSelected);
                    // if it is not on the map, maybe make the cursor invisible ?
                    //playerCursor.Visibility = Visibility.Visible;
                    Canvas.SetLeft(playerCursor, coord.Item1);
                    Canvas.SetTop(playerCursor, coord.Item2);
                    selectedTileChanged();
                }

            }
        }

        private void endTurn_clicked(object sender, RoutedEventArgs e)
        {
            //Show graphic elements 
            _gManager.nextTurn();
            fillGeneralInfo();
            gameEnd();
        }
        private void giveUp_clicked(object sender, RoutedEventArgs e)
        {
            resetAll();
        }

        private void resetAll()
        {
            _gManager = null; // not enough i presume
            _unitsOnTile = new List<SmallWorld.Unit>();
            // that way we don't remove the cursor
            _visualUnitsElements.Values.ToList().ForEach(u => mapControl.Children.Remove(u));
            _visualUnitsElements.Clear();
        }
        private void gameEnd()
        {
            if (_gManager.gameEnd())
            {
                gameView.Visibility = Visibility.Hidden;
                endGameMenu.Visibility = Visibility.Visible;
                resetAll();
            } 
        }

    }
}
