using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SmallWorld
{
    [Serializable]
    public abstract class Unit : SmallWorld.IUnit
    {
        protected static readonly Dictionary<TerrainType, Tuple<int, int>> terrainData = new Dictionary<TerrainType, Tuple<int, int>>();
        public static readonly int DEFAULT_MOVE_COST = 2;
        public static readonly int DEFAULT_POINT = 1;
        public static readonly int IMPOSSIBLE_MOVE = -1;

        public int Armour { get; private set; }
        public int Attack { get; private set; }
        public int Life { get; protected set; }
        public int MovesLeft { get; protected set; }
        public string Name { get; private set; }
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public int Value { get; protected set; }

        protected Unit(int posX = 0, int posY = 0, int movesLeft = 0, int armour = 1, int life = 5, int attack = 2, string name = "unnamed", int value = 0) {
            X = posX;
            Y = posY;
            MovesLeft = movesLeft;
            Armour = armour;
            Life = life;
            Attack = attack;
            Name = name;
            Value = value;
        }
        public virtual void move(int x, int y, Tile t, IEnumerable<Unit> advList)
        {
            // Diagram says movepossible, but that way movecost is used only once
            int cost = moveCost(x, y, t);
            if (cost != IMPOSSIBLE_MOVE && cost <= MovesLeft) {
                MovesLeft -= cost;

                //Search strongest unit
                //We could make a new list with the strongest and rand on it ... is it worth it performance wise ?
                int max = 0;
                Unit adv = null;
                foreach (Unit unit in advList ){
                    if (unit.Life > max) {
                        adv = unit;
                        max = unit.Life;
                    }
                }
                if (adv != null)
                {
                    fight(adv);
                    if (adv.Life == 0 && advList.Count() == 1)
                    {
                        setPosition(x, y);
                    }  
                }
                else
                {
                    setPosition(x, y);
                }
            }
        }

        public virtual void die() {}

        public virtual void fight(Unit opponent)
        {
            bool died = false;
            Random r = new Random();
            // + 3 because max is not included
            int nbTurns = r.Next(3, Math.Max(opponent.Life, Life) + 3);
            for (int i = 0; i < nbTurns && !died; ++i)
            {
                died = fightRound(opponent);
            }

        }
        //return true if someone died
        public virtual bool fightRound(Unit opponent) {
            int atk = Attack * Life / 5;
            int def = opponent.Armour * opponent.Life / 5;
            double seuil = (atk - def) * 12.5 + 50;
            Random rand = new Random();
            if (rand.Next(0, 101) > seuil) {
                opponent.damage();
                return opponent.Life == 0;
            }
            else {
                damage();
                return Life == 0;
            }

        }
        public virtual Dictionary<TerrainType, Tuple<int, int>> getTerrainData()
        {
            return Unit.terrainData;
        }

        //could use GameManager.Instance
        public virtual int scorePoints(Tile t)
        {
            Tuple<int, int> val;
            if (this.getTerrainData().TryGetValue(t.TerrainType, out val))
            {
                return val.Item2;
            }
            else
            {
                return DEFAULT_POINT;
            }
            
        }

        public virtual void damage()
        {
            Life--;
            if (Life == 0) {
                die();
            }
        }

        public virtual void startTurn()
        {
            MovesLeft = 4;
        }

        //result = -1 if move impossible
        public virtual int moveCost(int x, int y, Tile t)
        {
            int deltaX = Math.Abs(X - x);
            int deltaY = Math.Abs(Y - y);
            Tuple<int, int> val;
            // A simplification must exist
            if (deltaX <= 1 && deltaY <= 1 && (deltaY == 0 || (Y % 2 == 1 && (x - X) >= 0) || (Y % 2 == 0 && (x - X) <= 0)))
            {
                if (this.getTerrainData().TryGetValue(t.TerrainType, out val))
                {
                    return val.Item1;
                }
                else
                {
                    return DEFAULT_MOVE_COST;
                }
            }
            else
            {
                return IMPOSSIBLE_MOVE;
            }    
        }


        public void setPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        
        public Tile whereAmI()
        {
            return GameManager.Instance.Map.getTile(X, Y);
        }
    }
}
