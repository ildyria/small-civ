using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
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
        GameManager GManager { get; set; }
        //private Polygon _playerCursor;
        private Dictionary<Unit, UIElement> VisualUnitsElements { get; set; }
        private List<UIElement> AdvisedElements { get; set; }
        private Data DataValues { get; set; }
        
        
        // one way ticket to hell 
        private static Dictionary<Type, string> typeStyle = new Dictionary<Type, string>(){
            {typeof(SmallWorld.Elf), "elf_unit"},
            {typeof(SmallWorld.Orc), "orc_unit"},
            {typeof(SmallWorld.Dwarf), "dwarf_unit"} 
        };

        
        public MainWindow()
        {
            InitializeComponent();
            DataValues = new Data();
            //DataValues.CurrentUnit = new Dwarf(0, 0, 88, 12, 11, 1, "SATAN", 32);
            this.DataContext = DataValues;
            //_playerCursor = (Polygon)this.Resources["cursorHex"];
            DataValues.UnitsOnTile = new List<SmallWorld.Unit>();
            VisualUnitsElements = new Dictionary<SmallWorld.Unit,UIElement>();
            AdvisedElements = new List<UIElement>();
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
            GManager = gmn.makeGame();
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
            gmn.Names = new string[2] {p1Name.Text, p2Name.Text };
            gmn.Tribes =listTribes;
            gmn.setMapSize(mapSize);
            //gmn.setNbUnit(new int)
            GManager = gmn.makeGame();
            // RENDER
            //gameView.Visibility = Visibility.Visible;
            //createGameMenu.Visibility = Visibility.Collapsed;
            setUnitsOnMap();
            //Should start game instead
            GManager.getPlayer(1).play();
            fillGeneralInfo();
            
        }
        private void selectedTileChanged()
        {
            DataValues.UnitsOnTile = GManager.getAllUnits().FindAll(u => u.X == DataValues.ISelected && u.Y == DataValues.JSelected);
            DataValues.CurrentUnitNumber = 0;
            setEnableUnitsButtons();
        }

        public string PlayerTurn 
        { 
            get  {
                if (GManager != null)
                {
                    return "Joueur " + (GManager.PlayerTurn + 1);
                }
                return "ERROR";
            } 
        }
        //private void fillN
        private void fillGeneralInfo()
        {
            pointsJ1.Content = GManager.getPlayer(1).Points + " points";
            pointsJ2.Content = GManager.getPlayer(2).Points + " points";
        }

        private void fillTileInfo()
        {
            Tile t = DataValues.CurrentTile;
            //tileType.Content = t.toStringFR();
            Unit u = DataValues.CurrentPlayerUnit;
            //that way we get the bare cost
            tileMoveCost.Content = u.moveCost(u.X+1, u.Y, t);
            tilePoints.Content = u.scorePoints(t);
        }

        private void setEnableUnitsButtons()
        {
            if (DataValues.CurrentUnitNumber == 0)
            {
                buttonPrecUnit.IsEnabled = false;
            }
            else
            {
                buttonPrecUnit.IsEnabled = true;
            }

            if (DataValues.CurrentUnitNumber == DataValues.UnitsOnTile.Count - 1 || DataValues.UnitsOnTile.Count == 0)
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
            DataValues.CurrentUnitNumber++;
            setEnableUnitsButtons();
        }
        private void precUnit_clicked(object sender, RoutedEventArgs e)
        {
            DataValues.CurrentUnitNumber--;
            setEnableUnitsButtons();
        }
        private void setUnitsOnMap()
        {
            foreach (SmallWorld.Unit u in GManager.getAllUnits())
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
            if (DataValues.UnitsOnTile.Count != 0 && i < GManager.Map.SizeX && j < GManager.Map.SizeY && i >= 0 && j >= 0)
            {
                SmallWorld.Unit currentUnit = DataValues.UnitsOnTile[DataValues.CurrentUnitNumber];
                GManager.moveUnit(currentUnit, i, j);

                if (GManager.getAllUnits().Count != VisualUnitsElements.Count) // not optimized at all
                {
                    List<SmallWorld.Unit> toDel = VisualUnitsElements.Keys.ToList().FindAll(u => !GManager.getAllUnits().Contains(u));
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
            if (i < GManager.Map.SizeX && j < GManager.Map.SizeY && i >= 0 && j >= 0)
            {
                
                foreach (UIElement elem in AdvisedElements)
                {
                    mapControl.Children.Remove(elem);
                }
                AdvisedElements.Clear();

                DataValues.ISelected = i;
                DataValues.JSelected = j;
                //Tuple<double, double> coord = BoardView.indexToCoord(i, j);
                //Canvas.SetLeft(playerCursor, coord.Item1);
                //Canvas.SetTop(playerCursor, coord.Item2);
                selectedTileChanged();
                fillTileInfo();
                showAdvisedTiles();
            }
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mapView.IsMouseOver)
            {
                if (DataValues.UnitsOnTile.Count != 0) // checked twice, for compat
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
            GManager.nextTurn();
            fillGeneralInfo();
            DataValues.updateManager();
            gameEnd();
        }
        private void giveUp_clicked(object sender, RoutedEventArgs e)
        {
            resetAll();
        }

        private void resetAll()
        {
            GManager = null; // not enough i presume
            DataValues.UnitsOnTile = new List<SmallWorld.Unit>();
            // that way we don't remove the cursor
            VisualUnitsElements.Values.ToList().ForEach(u => mapControl.Children.Remove(u));
            VisualUnitsElements.Clear();
        }
        private void gameEnd()
        {
            if (GManager.gameEnd())
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
            if (DataValues.UnitsOnTile.Count != 0 && GManager.getCurrentPlayer().UnitList.Contains(DataValues.UnitsOnTile[DataValues.CurrentUnitNumber])) {
                Wrapper.WrapperGenMap g = GManager.MapAlgo;
                SmallWorld.Unit u = DataValues.UnitsOnTile[DataValues.CurrentUnitNumber];
                List<int> movesPossibles = new List<int>();
                for (int x = Math.Max(u.X - 1, 0); x <= Math.Min(u.X, GManager.Map.SizeX - 1); x++ )
                {
                    for (int y = Math.Max(u.Y - 1, 0); y <= Math.Min(u.Y + 1, GManager.Map.SizeX - 1); y++)
                    {
                        if (u.moveCost(x, y, GManager.Map.getTile(x, y)) > 0)
                        {
                            movesPossibles.Add(x * GManager.Map.SizeX + y);
                        }
                    }
                }

                Dictionary<int, Tuple<int, int>> terrainData = new Dictionary<int, Tuple<int, int>>();
                foreach(KeyValuePair<SmallWorld.TerrainType, Tuple<int, int>> entry in u.getTerrainData())
                {
                    terrainData.Add((int)entry.Key, entry.Value);
                }
                List<Tuple<int, int, int>> listOpponents = new List<Tuple<int, int, int>>();
                GManager.opponent().UnitList.ForEach(w => listOpponents.Add(new Tuple<int, int, int>(w.X, w.Y, w.Life)));
                List<int> best = g.bestMoves(3, new Tuple<int, int, int>(u.X, u.Y, u.Life), movesPossibles, terrainData, listOpponents);
                foreach (int move in best)

                {
                    Label adt = new Label();
                    int i = move / GManager.Map.SizeX;
                    int j = move % GManager.Map.SizeX;
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataValues.UnitsOnTile.Count != 0)
            {
                SmallWorld.Unit u = DataValues.UnitsOnTile[DataValues.CurrentUnitNumber];
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
            List<SmallWorld.Unit> lu = GManager.getCurrentPlayer().UnitList;
            if (DataValues.UnitsOnTile.Count != 0) // if an unit is selected
            {
                uNext = lu[(lu.IndexOf(DataValues.UnitsOnTile[DataValues.CurrentUnitNumber]) + 1) % lu.Count];
            }
            else 
            {
                uNext = GManager.getCurrentPlayer().UnitList.First();
            }


            moveCursor(uNext.X, uNext.Y);
            DataValues.CurrentUnitNumber = DataValues.UnitsOnTile.IndexOf(uNext);
            setEnableUnitsButtons();
        }
    }

    public class Data : INotifyPropertyChanged
    {
        private List<Unit> _unitsOnTile;
        private int _currentUnitNumber;
        private int _iSelected;
        private int _jSelected;

        GameManager GManager {
            get { return GameManager.Instance; } 
        }
        public double XCursor
        {
            get { return BoardView.indexToCoord(ISelected, JSelected).Item1; }
        }
        public double YCursor
        {
            get { return BoardView.indexToCoord(ISelected, JSelected).Item2; }
        }
        public int ISelected 
        {
            get { return _iSelected; }
            set { _iSelected = value; OnPropertyChanged("Xcursor");  }
        }
        public int JSelected 
        {
            get { return _jSelected; }
            set { _jSelected = value; OnPropertyChanged("Xcursor"); OnPropertyChanged("Ycursor"); }
        }
        public Tile CurrentTile
        {
            get { return GManager.Map.getTile(ISelected, JSelected); }
        }
        public Unit CurrentPlayerUnit
        {
            get { return GManager.getCurrentPlayer().UnitList[0]; }
        }

        public List<Unit> UnitsOnTile
        {
            get { return _unitsOnTile; }
            set { _unitsOnTile = value; OnPropertyChanged("CurrentUnit"); }
        }
        public int CurrentUnitNumber
        {
            get { return _currentUnitNumber; }
            set { _currentUnitNumber = value; OnPropertyChanged("CurrentUnit"); }
        }
        public Unit CurrentUnit
        {
            get 
            {
                if (UnitsOnTile != null && UnitsOnTile.Count > CurrentUnitNumber)
                {
                    return UnitsOnTile[CurrentUnitNumber]; 
                }
                // need a good way for resetting fields
                /*
                currentUnitListPosition.Content = "";
                name.Content = "";
                life.Content = "";
                movesLeft.Content = "";
                return null;
                */
                return new Dwarf(0, 0, 0, 0, 0, 0, "SATAN", 0);
                
            }
        }
        public void updateManager()
        {
            // does not work ...
            OnPropertyChanged("GManager");
        }
        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
