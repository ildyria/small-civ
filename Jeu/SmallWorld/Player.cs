using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    [Serializable]
    public class Player : SmallWorld.IPlayer
    {
        public int Points
        {
            get
            {
                Unit u = UnitList.First();
                int total = 0;
                foreach (int i in MovesList) 
                {
                    total += u.scorePoints(GameManager.Instance.Map.getTile(i));
                }
                return total;
            }
        }
        public string Tribe { get; private set; }
        public string Name { get; private set; }
        public List<Unit> UnitList { get; set; }
        public List<int> MovesList { get; set; }

        public Player(string name, string tribe, int points)
        {
            Name = name;
            Tribe = tribe;
            //Points = points;
            MovesList = new List<int>();
        }
        public void scorePoints()
        {
            foreach (Unit u in UnitList) {
                //Points += u.scorePoints(GameManager.Instance.Map.getTile(u.X, u.Y));
                MovesList.Add(u.Y * GameManager.Instance.Map.SizeX + u.X);
            }
        }

        public bool moveUnit(Unit u, int x, int y)
        {
            if (UnitList.Contains(u))
            {
                bool moved = GameManager.Instance.moveUnit(u, x, y);
                if (moved)
                {
                    MovesList.Add(y * GameManager.Instance.Map.SizeX + x);
                }
                bool end = GameManager.Instance.gameEnd();
            }
            return true;
        }

        public void deleteUnit(Unit u)
        {
            //Perhaps call the end of game ?
            UnitList.Remove(u);
        }

        public void play()
        {
            UnitList.ForEach(delegate(Unit u) { u.startTurn(); });
            //throw new System.NotImplementedException();
            //The rest when we get the interface
        }
        public List<Unit> unitsAt(int x, int y)
        {
            return UnitList.FindAll(u => u.X == x && u.Y == y);
        }
    }
}
