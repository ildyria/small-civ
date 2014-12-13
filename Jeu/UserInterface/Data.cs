using System;
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

        private List<Unit> _unitsOnTile;
        private int _currentUnitNumber;
        private int _iSelected;
        private int _jSelected;

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
        public int ISelected
        {
            get { return _iSelected; }
            set { _iSelected = value; OnPropertyChanged("Xcursor"); }
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
