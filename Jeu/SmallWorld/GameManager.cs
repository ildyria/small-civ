using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class GameManager : SmallWorld.IGameManager
    {
        private static GameManager _instance;

        private int _turnCurrent;
        private int _turnNumber;
        private int _playerTurn;
        private Player _player1;
        private Player _player2;
        private GameMap _map;

        private GameManager() { }
        public static void  init(Player p1, Player p2, GameMap map, int nbTurns, int turn, int playerTurn)
        {
            _instance = new GameManager();
            _instance._player1 = p1;
            _instance._player2 = p2;
            _instance._map = map;
            _instance._turnCurrent = turn;
            _instance._turnNumber = nbTurns;
            _instance._playerTurn = playerTurn;
        }

        public static GameManager Instance() {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }

        //The following is not static. It should. But i have no chocolate, so i can't change that.
        public GameMap getMap()
        {
            return _map;
        }
        public void setMap(GameMap map)
        {
            _map = map;
        }

        public int getTurnNumber()
        {
            return _turnNumber;
        }
        public int getTurnCurrent()
        {
            return _turnCurrent;
        }
        public int getPlayerTurn()
        {
            return _playerTurn;
        }
        public void setPlayer1(Player p)
        {
            _player1 = p;
        }
        public Player getPlayer1()
        {
            return _player1;
        }
        public void setPlayer2(Player p)
        {
            _player2 = p;
        }
        public Player getPlayer2()
        {
            return _player2;
        }

        public bool gameEnd()
        {
            if (_turnCurrent > _turnNumber || _player1.getUnits().Count == 0 || _player2.getUnits().Count == 0)
            {
                return true;
            }
            return false;
        }

        public void gameStart()
        {
            throw new System.NotImplementedException();
        }

        public List<Unit> getUnits()
        {
            List<Unit> allUnits = new List<Unit>();
            allUnits.Concat(_player1.getUnits()).Concat(_player2.getUnits());
            return allUnits;
        }

        public void computeFinalScore()
        {
            throw new System.NotImplementedException();
        }

        public void moveUnit(Unit u, int x, int y)
        {
            Tile start = _map.getTile(u.getX(), u.getY());
            Tile end = _map.getTile(x, y);
            List<Unit> opponents = null;
            Player adv = null;
            if (_playerTurn == 1) // Should be changed, array would be better
            {
                opponents = _player2.unitsAt(x, y);
                adv = _player2;
            }
            else
            {
                opponents = _player1.unitsAt(x, y);
                adv = _player1;
            }
            u.move(x, y, end, opponents);

            if (u.getLife() == 0)
            {

            }
            //another if, and not else if for compat with future updates.
            Unit op = opponents.Find(z => z.getLife() == 0);
            if (op != null) // does this work ?
            {
                adv.deleteUnit(op);
            }
        }
    }
}
