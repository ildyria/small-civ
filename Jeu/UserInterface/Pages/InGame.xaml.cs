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
using SmallWorld;

namespace UserInterface.Pages
{
    /// <summary>
    /// Logique d'interaction pour InGame.xaml
    /// </summary>
    public partial class InGame : UserControl, ISwitchable
    {
        // one way ticket to hell 
        private static Dictionary<Type, string> typeStyle = new Dictionary<Type, string>(){
            {typeof(SmallWorld.Elf), "elf_unit"},
            {typeof(SmallWorld.Orc), "orc_unit"},
            {typeof(SmallWorld.Dwarf), "dwarf_unit"} 
        };
        private Dictionary<Unit, UIElement> VisualUnitsElements { get; set; }
        private List<UIElement> AdvisedElements { get; set; }
        public InGame()
        {
            InitializeComponent();
            this.DataContext = Data.Instance;
            //Data.Instance.CurrentUnit = new Dwarf(0, 0, 88, 12, 11, 1, "SATAN", 32);
            //_playerCursor = (Polygon)this.Resources["cursorHex"];
            Data.Instance.UnitsOnTile = new List<SmallWorld.Unit>();
            VisualUnitsElements = new Dictionary<SmallWorld.Unit, UIElement>();
            AdvisedElements = new List<UIElement>();
            // RENDER;
            setUnitsOnMap();

        }

        private void setUnitsOnMap()
        {
            foreach (SmallWorld.Unit u in Data.Instance.GManager.getAllUnits())
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
                VisualUnitsElements.Add(u, asset);

            }
        }
        private void moveUnit(int i, int j)
        {
            if (Data.Instance.UnitsOnTile.Count != 0 && i < Data.Instance.GManager.Map.SizeX && j < Data.Instance.GManager.Map.SizeY && i >= 0 && j >= 0)
            {
                SmallWorld.Unit currentUnit = Data.Instance.UnitsOnTile[Data.Instance.CurrentUnitNumber];
                Data.Instance.GManager.moveUnit(currentUnit, i, j);

                if (Data.Instance.GManager.getAllUnits().Count != VisualUnitsElements.Count) // not optimized at all
                {
                    List<SmallWorld.Unit> toDel = VisualUnitsElements.Keys.ToList().FindAll(u => !Data.Instance.GManager.getAllUnits().Contains(u));
                    foreach (SmallWorld.Unit u in toDel)
                    {
                        mapControl.Children.Remove(VisualUnitsElements[currentUnit]);
                        VisualUnitsElements.Remove(currentUnit);
                    }
                }
                if (currentUnit.Life == 0)
                {
                    //already done earlier in the ugly method above
                    //mapControl.Children.Remove(VisualUnitsElements[currentUnit]);
                    //VisualUnitsElements.Remove(currentUnit);
                }
                else
                {
                    Tuple<double, double> coord = BoardView.indexToCoord(currentUnit.X, currentUnit.Y);
                    Canvas.SetLeft(VisualUnitsElements[currentUnit], coord.Item1);
                    Canvas.SetTop(VisualUnitsElements[currentUnit], coord.Item2);
                }
            }
        }
        private void moveCursor(int i, int j)
        {
            // no move if not on grid
            // if it is not on the map, maybe make the cursor invisible ?
            if (i < Data.Instance.GManager.Map.SizeX && j < Data.Instance.GManager.Map.SizeY && i >= 0 && j >= 0)
            {

                foreach (UIElement elem in AdvisedElements)
                {
                    mapControl.Children.Remove(elem);
                }
                AdvisedElements.Clear();

                Data.Instance.ISelected = i;
                Data.Instance.JSelected = j;
                selectedTileChanged();
                fillTileInfo();
                showAdvisedTiles();
            }
        }
        private void selectedTileChanged()
        {
            Data.Instance.UnitsOnTile = Data.Instance.GManager.getAllUnits().FindAll(u => u.X == Data.Instance.ISelected && u.Y == Data.Instance.JSelected);
            Data.Instance.CurrentUnitNumber = 0;
            setEnableUnitsButtons();
        }

        public string PlayerTurn 
        { 
            get  {
                if (Data.Instance.GManager != null)
                {
                    return "Joueur " + (Data.Instance.GManager.PlayerTurn + 1);
                }
                return "ERROR";
            } 
        }
        //private void fillN
        private void fillGeneralInfo()
        {
            pointsJ1.Content = Data.Instance.GManager.getPlayer(1).Points + " points";
            pointsJ2.Content = Data.Instance.GManager.getPlayer(2).Points + " points";
        }

        private void fillTileInfo()
        {
            Tile t = Data.Instance.CurrentTile;
            //tileType.Content = t.toStringFR();
            Unit u = Data.Instance.CurrentPlayerUnit;
            //that way we get the bare cost
            tileMoveCost.Content = u.moveCost(u.X+1, u.Y, t);
            tilePoints.Content = u.scorePoints(t);
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mapView.IsMouseOver)
            {
                if (Data.Instance.UnitsOnTile.Count != 0) // checked twice, for compat
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
        private void gameEnd()
        {
            if (Data.Instance.GManager.gameEnd())
            {
                Switcher.Switch(new Pages.EndGameMenu());
                resetAll();
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Data.Instance.UnitsOnTile.Count != 0)
            {
                SmallWorld.Unit u = Data.Instance.UnitsOnTile[Data.Instance.CurrentUnitNumber];
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

        private void nextUnit()
        {
            SmallWorld.Unit uNext;
            List<SmallWorld.Unit> lu = Data.Instance.GManager.getCurrentPlayer().UnitList;
            if (Data.Instance.UnitsOnTile.Count != 0) // if an unit is selected
            {
                uNext = lu[(lu.IndexOf(Data.Instance.UnitsOnTile[Data.Instance.CurrentUnitNumber]) + 1) % lu.Count];
            }
            else
            {
                uNext = Data.Instance.GManager.getCurrentPlayer().UnitList.First();
            }


            moveCursor(uNext.X, uNext.Y);
            Data.Instance.CurrentUnitNumber = Data.Instance.UnitsOnTile.IndexOf(uNext);
            setEnableUnitsButtons();
        }

        private void endTurn_clicked(object sender = null, RoutedEventArgs e = null)
        {
            //Show graphic elements 
            Data.Instance.GManager.nextTurn();
            fillGeneralInfo();
            Data.Instance.updateManager();
            gameEnd();
        }
        private void giveUp_clicked(object sender, RoutedEventArgs e)
        {
            resetAll();
            Switcher.Switch(new Pages.MainMenu()); // perhaps end screen ?
        }

        private void resetAll()
        {
            //Data.Instance.GManager = null; // not enough i presume
            Data.Instance.UnitsOnTile = new List<SmallWorld.Unit>();
            // that way we don't remove the cursor
            VisualUnitsElements.Values.ToList().ForEach(u => mapControl.Children.Remove(u));
            VisualUnitsElements.Clear();
        }


        private void showAdvisedTiles()
        {
            if (Data.Instance.UnitsOnTile.Count != 0 && Data.Instance.GManager.getCurrentPlayer().UnitList.Contains(Data.Instance.UnitsOnTile[Data.Instance.CurrentUnitNumber]))
            {
                Wrapper.WrapperGenMap g = Data.Instance.GManager.MapAlgo;
                SmallWorld.Unit u = Data.Instance.UnitsOnTile[Data.Instance.CurrentUnitNumber];
                List<int> movesPossibles = new List<int>();
                for (int x = Math.Max(u.X - 1, 0); x <= Math.Min(u.X, Data.Instance.GManager.Map.SizeX - 1); x++)
                {
                    for (int y = Math.Max(u.Y - 1, 0); y <= Math.Min(u.Y + 1, Data.Instance.GManager.Map.SizeX - 1); y++)
                    {
                        if (u.moveCost(x, y, Data.Instance.GManager.Map.getTile(x, y)) > 0)
                        {
                            movesPossibles.Add(x * Data.Instance.GManager.Map.SizeX + y);
                        }
                    }
                }

                Dictionary<int, Tuple<int, int>> terrainData = new Dictionary<int, Tuple<int, int>>();
                foreach (KeyValuePair<SmallWorld.TerrainType, Tuple<int, int>> entry in u.getTerrainData())
                {
                    terrainData.Add((int)entry.Key, entry.Value);
                }
                List<Tuple<int, int, int>> listOpponents = new List<Tuple<int, int, int>>();
                Data.Instance.GManager.opponent().UnitList.ForEach(w => listOpponents.Add(new Tuple<int, int, int>(w.X, w.Y, w.Life)));
                List<int> best = g.bestMoves(3, new Tuple<int, int, int>(u.X, u.Y, u.Life), movesPossibles, terrainData, listOpponents);
                foreach (int move in best)
                {
                    Label adt = new Label();
                    int i = move / Data.Instance.GManager.Map.SizeX;
                    int j = move % Data.Instance.GManager.Map.SizeX;
                    adt.Style = App.Current.FindResource("advisedTile") as Style;
                    Tuple<double, double> adtPos = BoardView.indexToCoord(i, j);
                    Canvas.SetLeft(adt, adtPos.Item1);
                    Canvas.SetTop(adt, adtPos.Item2);
                    Canvas.SetZIndex(adt, 2);
                    mapControl.Children.Add(adt);
                    AdvisedElements.Add(adt);
                }
            }

        }

        private void setEnableUnitsButtons()
        {
            if (Data.Instance.CurrentUnitNumber == 0)
            {
                buttonPrecUnit.IsEnabled = false;
            }
            else
            {
                buttonPrecUnit.IsEnabled = true;
            }

            if (Data.Instance.CurrentUnitNumber == Data.Instance.UnitsOnTile.Count - 1 || Data.Instance.UnitsOnTile.Count == 0)
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
            Data.Instance.CurrentUnitNumber++;
            setEnableUnitsButtons();
        }
        private void precUnit_clicked(object sender, RoutedEventArgs e)
        {
            Data.Instance.CurrentUnitNumber--;
            setEnableUnitsButtons();
        }



        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        private void saveLoadButton_Click(object sender, RoutedEventArgs e)
        {
            // need to implement origin
            Switcher.Switch(new Pages.LoadPage());
        }
        #endregion

        
    }
}
