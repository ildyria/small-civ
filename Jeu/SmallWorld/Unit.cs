﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SmallWorld
{
    public abstract class Unit : SmallWorld.IUnit
    {
        protected static readonly Dictionary<TerrainType, Tuple<int, int>> terrainData = new Dictionary<TerrainType, Tuple<int, int>>();
        static int DEFAULT_MOVE_COST = 2;
        static int DEFAULT_POINT = 1;

        protected int _posX;
        protected int _posY;
        protected int _movesLeft;
        protected int _armour;
        protected int _life;
        protected int _attack;
        protected string _name;
        protected int _value;

        protected Unit(int posX = 0, int posY = 0, int movesLeft = 0, int armour = 1, int life = 5, int attack = 2, string name = "unnamed", int value = 0) {
            _posX = posX;
            _posY = posY;
            _movesLeft = movesLeft;
            _armour = armour;
            _life = life;
            _attack = attack;
            _name = name;
            _value = value;
        }
        public virtual void move(int x, int y, Tile t, IEnumerable<Unit> advList)
        {
            // Le diagramme prévois un movepossible, mais on le fait ici à la main pour ne pas calculer le cout de fois.
            int cost = moveCost(x, y, t);
            if (cost != 0) {
                _movesLeft -= cost;

                //recherche de l'unité la plus forte
                //Ce n'es pas un hasard ... ordre random ?
                int max = 0;
                Unit adv = null;
                foreach (Unit unit in advList ){
                    if (unit.getLife() > max) {
                        adv = unit;
                        max = unit.getLife();
                    }
                }
                if (adv != null)
                {
                    fight(adv);
                    if (adv.getLife() == 0 && advList.Count() == 1)
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
            int nbTurns = r.Next(3, Math.Max(opponent.getLife(), _life) + 3);
            for (int i = 0; i < nbTurns && !died; ++i)
            {
                died = fightRound(opponent);
            }

        }
        //return true if someone died
        public virtual bool fightRound(Unit opponent) {
            int atk = _attack * _life / 5;
            int def = opponent.getArmour() * opponent.getLife() / 5;
            double seuil = (atk - def) * 12.5 + 50;
            Random rand = new Random();
            if (rand.Next(0, 100) > seuil) {
                opponent.damage();
                return opponent.getLife() == 0;
            }
            else {
                damage();
                return _life == 0;
            }

        }
        public virtual Dictionary<TerrainType, Tuple<int, int>> getTerrainData()
        {
            return Unit.terrainData;
        }
        public virtual int scorePoints(Tile t)
        {
            Tuple<int, int> val;
            if (this.getTerrainData().TryGetValue(t.getType(), out val))
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
            _life--;
            if (_life == 0) {
                die();
            }
        }

        public virtual void startTurn()
        {
            _movesLeft = 4;
        }

        //result = -1 if move impossible
        public virtual int moveCost(int x, int y, Tile t)
        {
            int deltaX = Math.Abs(_posX - x);
            int deltaY = Math.Abs(_posY - y);
            Tuple<int, int> val;
            if (deltaX <= 1 && deltaY <= 1)
            {
                if (this.getTerrainData().TryGetValue(t.getType(), out val))
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
                return -1;
            }    
        }


        public void setPosition(int x, int y)
        {
            _posX = x;
            _posY = y;
        }

        public int getArmour()
        {
            return _armour;
        }

        public int getAttack()
        {
            return _attack;
        }

        public int getLife()
        {
            return _life;
        }

        public int getMovesLeft()
        {
            return _movesLeft;
        }

        public string getName()
        {
            return _name;
        }

        public int getX()
        {
            return _posX;
        }

        public int getY()
        {
            return _posY;
        }

        public int getValue()
        {
            return _value;
        }
    }
}