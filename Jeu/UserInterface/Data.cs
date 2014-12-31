using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using SmallWorld;

namespace UserInterface
{
    public class Data : INotifyPropertyChanged
    {
        private static Data _instance;
        public static Data Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Data();
                }
                return _instance;
            }
        }

        public string NumUnitsOnTotal
        {
            get 
            {
                if (UnitsOnTile.Count != 0)
                {
                    return (CurrentUnitNumber + 1) + "/" + UnitsOnTile.Count; 
                }
                return null;
            }
        }

        public string CurrentTurn
        {
            get { return GManager.TurnCurrent + "/" + GManager.TurnNumber; }
        }
        public string CurrentPlayer
        {
            get
            {
                if (Data.Instance.GManager != null)
                {
                    return GManager.Players[GManager.PlayerTurn].Name;
                }
                return "ERROR";
            }
        }
        public Boolean ButtonPrecEnabled
        {
            get { return CurrentUnitNumber > 0; }
        }

        public Style StyleMenuPlayer1
        {
            get { return (GManager.PlayerTurn == 0) ? (Style)App.Current.FindResource("currentPlayerStyle") : (Style)App.Current.FindResource("defaultPlayerStyle"); }
        }
        public Style StyleMenuPlayer2
        {
            get { return (GManager.PlayerTurn == 1) ? (Style)App.Current.FindResource("currentPlayerStyle") : (Style)App.Current.FindResource("defaultPlayerStyle"); }
        }
        public Boolean ButtonNextEnabled
        {
            get { return (Data.Instance.CurrentUnitNumber < Data.Instance.UnitsOnTile.Count - 1 && Data.Instance.UnitsOnTile.Count != 0); }
        }

        public string CurrentTileName
        {
            get { return CurrentTile.toStringFR();  }
        }

        public int CurrentMoveCost
        {
            get { return CurrentPlayerUnit.tileMoveCost(CurrentTile); }
        }
        public int CurrentTilePoints
        {
            get { return CurrentPlayerUnit.scorePoints(CurrentTile); }
        }

        public bool FromGame { get; set; }
        private List<Unit> _unitsOnTile;
        private int _currentUnitNumber;
        private int _iSelected;
        private int _jSelected;

        [DependentProperties("StyleMenuPlayer1", "StyleMenuPlayer2", "CurrentTurn", "CurrentPlayer")]
        public GameManager GManager
        {
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

        [DependentProperties("XCursor", "CurrentTile", "NumUnitsOnTotal")]
        public int ISelected
        {
            get { return _iSelected; }
            set { _iSelected = value; RaiseProperty("ISelected"); }
        }
        [DependentProperties("XCursor", "Ycursor", "CurrentTile", "NumUnitsOnTotal")]
        public int JSelected
        {
            get { return _jSelected; }
            set { _jSelected = value; RaiseProperty("JSelected"); }
        }
        [DependentProperties("CurrentTileName", "CurrentMoveCost", "CurrentTilePoints", "UnitsOnTile", "CurrentUnitNumber")]
        public Tile CurrentTile
        {
            get { return GManager.Map.getTile(ISelected, JSelected); }
        }
        public Unit CurrentPlayerUnit
        {
            get { return GManager.getCurrentPlayer().UnitList[0]; }
        }

        [DependentProperties("CurrentUnit", "ButtonPrecEnabed", "ButtonNextEnabled", "NumUnitsOnTotal")]
        public List<Unit> UnitsOnTile
        {
            get { return _unitsOnTile; }
            set { _unitsOnTile = value; RaiseProperty("UnitsOnTile"); }
        }
        [DependentProperties("CurrentUnit", "ButtonPrecEnabled", "ButtonNextEnabled", "NumUnitsOnTotal")]
        public int CurrentUnitNumber
        {
            get { return _currentUnitNumber; }
            set { _currentUnitNumber = value; RaiseProperty("CurrentUnitNumber"); }
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
                // null does the job
                return null;

            }
        }
        public void updateManager()
        {
            // does not work ...
            RaiseProperty("GManager");
        }







        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        protected void RaiseProperty(string propertyName, List<string> calledProperties = null)
        {
            OnPropertyChanged(propertyName);

            if (calledProperties == null)
            {
                calledProperties = new List<string>();
            }

            calledProperties.Add(propertyName);

            PropertyInfo pInfo = GetType().GetProperty(propertyName);

            if (pInfo != null)
            {
                foreach (DependentPropertiesAttribute ca in pInfo.GetCustomAttributes(false).OfType<DependentPropertiesAttribute>())
                {
                    if (ca.Properties != null)
                    {
                        foreach (string prop in ca.Properties)
                        {
                            if (prop != propertyName && !calledProperties.Contains(prop))
                            {
                                RaiseProperty(prop, calledProperties);
                            }
                        }
                    }
                }
            }
        }

    }



    public class DependentPropertiesAttribute : Attribute
    {
        private readonly string[] properties;

        public DependentPropertiesAttribute(params string[] dp)
        {
            properties = dp;
        }

        public string[] Properties
        {
            get
            {
                return properties;
            }
        }
    }
}
