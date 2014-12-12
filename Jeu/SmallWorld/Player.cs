using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class Player : SmallWorld.IPlayer
    {
        public int Points { get; private set; }
        public string Tribe { get; private set; }
        public string Name { get; private set; }
        public List<Unit> UnitList { get; set; }


        public Player(string name, string tribe, int points)
        {
            Name = name;
            Tribe = tribe;
            Points = points;
        }
        public void scorePoints()
        {
            foreach (Unit u in UnitList) {
                Points += u.scorePoints(GameManager.Instance().Map.getTile(u.X, u.Y));
            }
            
        }

        public void moveUnit(Unit u, int x, int y)
        {
            if (UnitList.Contains(u))
            {
                GameManager.Instance().moveUnit(u, x, y);
                bool end = GameManager.Instance().gameEnd();
            }
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
