﻿using System;
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
        private List<UIElement> _advisedElements;
        // one way ticket to hell 
        private static Dictionary<Type, string> typeStyle = new Dictionary<Type, string>(){
            {typeof(SmallWorld.Elf), "elf_unit"},
            {typeof(SmallWorld.Orc), "orc_unit"},
            {typeof(SmallWorld.Dwarf), "dwarf_unit"} 
        };

        
        public MainWindow()
        {
            InitializeComponent();
            //_playerCursor = (Polygon)this.Resources["cursorHex"];
            _unitsOnTile = new List<SmallWorld.Unit>();
            _visualUnitsElements = new Dictionary<SmallWorld.Unit,UIElement>();
            _advisedElements = new List<UIElement>();
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
            _unitsOnTile = _gManager.getAllUnits().FindAll(u => u.X == _iSelected && u.Y == _jSelected);
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
               name.Content = unitToDetail.Name;
               life.Content = unitToDetail.Life;
               movesLeft.Content = unitToDetail.MovesLeft;
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
            pointsJ1.Content = _gManager.getPlayer(1).Points + " points";
            pointsJ2.Content = _gManager.getPlayer(2).Points + " points";
            turn.Content = _gManager.TurnCurrent + "/" + _gManager.TurnNumber;
            playerTurn.Content = "Joueur " + (_gManager.PlayerTurn + 1);
        }

        private void fillTileInfo()
        {
            Tile t = _gManager.Map.getTile(_iSelected, _jSelected);
            tileType.Content = t.toStringFR();
            Unit u = _gManager.getCurrentPlayer().UnitList[0];
            //that way we get the bare cost
            tileMoveCost.Content = u.moveCost(u.X+1, u.Y, t);
            tilePoints.Content = u.scorePoints(t);
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
            foreach (SmallWorld.Unit u in _gManager.getAllUnits())
            {
                Label asset = new Label();
                asset.Style = App.Current.FindResource(typeStyle[u.GetType()]) as Style;
                
                //to convert to true coord
                Tuple<double, double> pos = BoardView.indexToCoord(u.X, u.Y);
                Canvas.SetLeft(asset, pos.Item1);
                Canvas.SetTop(asset, pos.Item2);
                //mapControl.Children.Insert(2, rect);
                Canvas.SetZIndex(asset, 3);
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

        private void moveUnit(int i, int j)
        {
            if (_unitsOnTile.Count != 0 && i < _gManager.Map.getSize().Item1 && j < _gManager.Map.getSize().Item2 && i >= 0 && j >= 0)
            {
                SmallWorld.Unit currentUnit = _unitsOnTile[_currentUnitNumber];
                _gManager.moveUnit(currentUnit, i, j);

                if (_gManager.getAllUnits().Count != _visualUnitsElements.Count) // not optimized at all
                {
                    List<SmallWorld.Unit> toDel = _visualUnitsElements.Keys.ToList().FindAll(u => !_gManager.getAllUnits().Contains(u));
                    foreach (SmallWorld.Unit u in toDel)
                    {
                        mapControl.Children.Remove(_visualUnitsElements[currentUnit]);
                        _visualUnitsElements.Remove(currentUnit);
                    }
                }
                if (currentUnit.Life == 0)
                {
                    //already done earlier in the ugly method above
                    //mapControl.Children.Remove(_visualUnitsElements[currentUnit]);
                    //_visualUnitsElements.Remove(currentUnit);
                }
                else 
                {
                    Tuple<double, double> coord = BoardView.indexToCoord(currentUnit.X, currentUnit.Y);
                    Canvas.SetLeft(_visualUnitsElements[currentUnit], coord.Item1);
                    Canvas.SetTop(_visualUnitsElements[currentUnit], coord.Item2);
                }
            }  
        }
        private void moveCursor(int i, int j)
        {
            // no move if not on grid
            // if it is not on the map, maybe make the cursor invisible ?
            if (i < _gManager.Map.getSize().Item1 && j < _gManager.Map.getSize().Item2 && i >= 0 && j >= 0)
            {
                
                foreach (UIElement elem in _advisedElements)
                {
                    mapControl.Children.Remove(elem);
                }
                _advisedElements.Clear();

                _iSelected = i;
                _jSelected = j;
                Tuple<double, double> coord = BoardView.indexToCoord(i, j);
                Canvas.SetLeft(playerCursor, coord.Item1);
                Canvas.SetTop(playerCursor, coord.Item2);
                selectedTileChanged();
                fillTileInfo();
                showAdvisedTiles();
            }
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mapView.IsMouseOver)
            {
                if (_unitsOnTile.Count != 0) // checked twice, for compat
                {
                    System.Windows.Point p = e.GetPosition(mapView);
                    Tuple<int, int> coord = BoardView.coordToIndex(p.X, p.Y);
                    moveUnitAndCursor(coord.Item1, coord.Item2);
                }
            }
        }
        private void moveUnitAndCursor(int i, int j)
        {
            moveUnit(i, j);
            gameEnd();
            moveCursor(i, j);
        }

        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mapView.IsMouseOver)
            {
                System.Windows.Point p = e.GetPosition(mapPanel);
                Tuple<int, int> posRel = BoardView.coordToIndex(p.X, p.Y);

                //System.Diagnostics.Trace.WriteLine(x + " " + y);
                
                //playerCursor.Visibility = Visibility.Visible;
                moveCursor(posRel.Item1, posRel.Item2);
            }
        }

        private void endTurn_clicked(object sender = null, RoutedEventArgs e = null)
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
                mapView.Visibility = Visibility.Collapsed;
                statusBar.Visibility = Visibility.Collapsed;
                gameControl.Visibility = Visibility.Collapsed;
                mapControl.Visibility = Visibility.Collapsed;
                unitDetails.Visibility = Visibility.Collapsed;
                gameView.Visibility = Visibility.Collapsed;
                endGameMenu.Visibility = Visibility.Visible;
                resetAll();
            } 
        }

        private void showAdvisedTiles()
        {
            if (_unitsOnTile.Count != 0 && _gManager.getCurrentPlayer().UnitList.Contains(_unitsOnTile[_currentUnitNumber])) {
                Wrapper.WrapperGenMap g = _gManager.MapAlgo;
                SmallWorld.Unit u = _unitsOnTile[_currentUnitNumber];
                List<int> movesPossibles = new List<int>();
                for (int x = Math.Max(u.X - 1, 0); x <= Math.Min(u.X, _gManager.Map.getSize().Item1 - 1); x++ )
                {
                    for (int y = Math.Max(u.Y - 1, 0); y <= Math.Min(u.Y + 1, _gManager.Map.getSize().Item1 - 1); y++)
                    {
                        if (u.moveCost(x, y, _gManager.Map.getTile(x, y)) > 0)
                        {
                            movesPossibles.Add(x * _gManager.Map.getSize().Item1 + y);
                        }
                    }
                }

                Dictionary<int, Tuple<int, int>> terrainData = new Dictionary<int, Tuple<int, int>>();
                foreach(KeyValuePair<SmallWorld.TerrainType, Tuple<int, int>> entry in u.getTerrainData())
                {
                    terrainData.Add((int)entry.Key, entry.Value);
                }
                List<Tuple<int, int, int>> listOpponents = new List<Tuple<int, int, int>>();
                _gManager.opponent().UnitList.ForEach(w => listOpponents.Add(new Tuple<int, int, int>(w.X, w.Y, w.Life)));
                List<int> best = g.bestMoves(3, new Tuple<int, int, int>(u.X, u.Y, u.Life), movesPossibles, terrainData, listOpponents);
                foreach (int move in best)

                {
                    Label adt = new Label();
                    int i = move / _gManager.Map.getSize().Item1;
                    int j = move % _gManager.Map.getSize().Item1;
                    adt.Style = App.Current.FindResource("advisedTile") as Style;
                    Tuple<double, double> adtPos = BoardView.indexToCoord(i, j);
                    Canvas.SetLeft(adt, adtPos.Item1);
                    Canvas.SetTop(adt, adtPos.Item2);
                    Canvas.SetZIndex(adt, 2);
                    mapControl.Children.Add(adt);
                    _advisedElements.Add(adt);
                }
            }
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (_unitsOnTile.Count != 0)
            {
                SmallWorld.Unit u = _unitsOnTile[_currentUnitNumber];
                int offset = u.Y % 2 == 0 ? -1 : 0;
                switch (e.Key)
                {
                    case Key.NumPad1:
                        moveUnitAndCursor(u.X + offset, u.Y + 1);
                        break;
                    case Key.NumPad3:
                        moveUnitAndCursor(u.X + offset + 1, u.Y + 1);
                        break;
                    case Key.NumPad4:
                        moveUnitAndCursor(u.X - 1, u.Y);
                        break;
                    case Key.NumPad6:
                        moveUnitAndCursor(u.X + 1, u.Y);
                        break;
                    case Key.NumPad7:
                        moveUnitAndCursor(u.X + offset, u.Y - 1);
                        break;
                    case Key.NumPad9:
                        moveUnitAndCursor(u.X + offset + 1, u.Y - 1);
                        break;
                    default:
                        break;
                }
            }

            switch (e.Key)
            { 
                case Key.Space:
                    nextUnit();
                    break;
                case Key.Enter:
                    endTurn_clicked();
                    break;
                default:
                    break;     
            }
        }

        private void nextUnit()
        {
            SmallWorld.Unit uNext;
            List<SmallWorld.Unit> lu = _gManager.getCurrentPlayer().UnitList;
            if (_unitsOnTile.Count != 0) // if an unit is selected
            {
                uNext = lu[(lu.IndexOf(_unitsOnTile[_currentUnitNumber]) + 1) % lu.Count];
            }
            else 
            {
                uNext = _gManager.getCurrentPlayer().UnitList.First();
            }


            moveCursor(uNext.X, uNext.Y);
            _currentUnitNumber = _unitsOnTile.IndexOf(uNext);
            setEnableUnitsButtons();
            fillUnitInfo();
        }
    }
}
