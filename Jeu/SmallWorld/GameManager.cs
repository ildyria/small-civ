using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class GameManager : SmallWorld.IGameManager
    {
        //Could also have been an argument of every function. Which is better ? 
        //A good way to decide would be to do that Rutger Hauer style but I don't want to declaim poetry on a rainy night on a rooftop with only underwear on.
        //Sorry if that seems a bit egoistic.
        private static GameManager _instance;

        private int _turnCurrent;
        private int _turnNumber;
        private int _playerTurn;
        private Player[] _players;
        private GameMap _map;

        private GameManager() { }
        public static void  init(Player p1, Player p2, GameMap map, int nbTurns, int turn, int playerTurn)
        {
            _instance = new GameManager();
            _instance._players = new Player[2] {p1, p2};
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
        public void setPlayer(int numPlayer, Player p)
        {
            _players[(numPlayer + 1) % 2] = p;
        }
        public Player getPlayer(int numPlayer)
        {
            return _players[(numPlayer + 1) % 2];
        }
        public void setPlayer1(Player p)
        {
            _players[0] = p;
        }
        public Player getPlayer1()
        {
            return _players[0];
        }
        public void setPlayer2(Player p)
        {
            _players[1] = p;
        }
        public Player getPlayer2()
        {
            return _players[1];
        }

        public bool gameEnd()
        {
            if (_turnCurrent > _turnNumber || _players[0].getUnits().Count == 0 || _players[1].getUnits().Count == 0)
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
            return _players[0].getUnits().Concat(_players[1].getUnits()).ToList();
        }

        public void computeFinalScore()
        {
            throw new System.NotImplementedException();
        }
        public Player getCurrentPlayer()
        {
            return _players[_playerTurn];
        }
        public void moveUnit(Unit u, int x, int y)
        {
            // You can only move units during your turn
            if (getCurrentPlayer().getUnits().Contains(u))
            {
                Tile start = _map.getTile(u.getX(), u.getY());
                Tile end = _map.getTile(x, y);
                List<Unit> opponents = _players[(_playerTurn + 1) % 2].unitsAt(x, y);
                Player adv = _players[(_playerTurn + 1) % 2];

                u.move(x, y, end, opponents);

                if (u.getLife() == 0)
                {
                    getCurrentPlayer().deleteUnit(u);
                }
                //another if, and not else if for compat with future updates.
                Unit op = opponents.Find(z => z.getLife() == 0);
                if (op != null) // does this work ?
                {
                    adv.deleteUnit(op);
                    System.Diagnostics.Trace.WriteLine("");
                }
            }
        }
        public Player opponent()
        {
            return getPlayer((_playerTurn + 1) % 2);
        }
    }
}
