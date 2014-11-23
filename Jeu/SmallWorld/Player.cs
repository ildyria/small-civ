using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class Player : SmallWorld.IPlayer
    {
        private int _points;
        private string _tribe;
        private string _name;
        private List<Unit> _unitList;


        public Player(string name, string tribe, int points)
        {
            _name = name;
            _tribe = tribe;
            _points = points;
        }
        public void scorePoints(Tile t)
        {
            foreach (Unit u in _unitList) {
                _points += u.scorePoints(t);
            }
            
        }

        public void moveUnit(Unit u, int x, int y)
        {
            if (_unitList.Contains(u))
            {
                // GameNamager call, should be static
                GameManager gm = null;
                gm.moveUnit(u, x, y);
                //Something like this.
                bool end = gm.gameEnd();
                //move(u, x, y);
            }
        }

        public void deleteUnit(Unit u)
        {
            //Perhaps call the end of game ?
            _unitList.Remove(u);
        }

        public List<Unit> getUnits()
        {
            return _unitList;
        }

        public void play()
        {
            _unitList.ForEach(delegate(Unit u) { u.startTurn(); });
            //throw new System.NotImplementedException();
            //The rest when we get the interface
        }

        public void setUnits(List<Unit> unitList)
        {
            _unitList = unitList;
        }

        public List<Unit> unitsAt(int x, int y)
        {
            List<Unit> units = new List<Unit>();
            foreach (Unit u in _unitList) {
                if (u.getX ()== x && u.getY() == y)
                {
                    units.Add(u);
                }
            }
            return units;
        }

        public string getName()
        {
            return _name;
        }

        public int getPoints()
        {
            return _points;
        }

        public string getTribe()
        {
            return _tribe;
        }
    }
}
