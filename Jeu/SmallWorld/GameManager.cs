using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class GameManager : SmallWorld.IGameManager
    {
        private int _turnCurrent;
        private int _turnNumber;
        private int _playerTurn;
        private Player _player1;
        private Player _player2;
        private GameMap _map;

        public GameManager(Player p1, Player p2, GameMap map, int nbTurns, int turn, int playerTurn)
        {
            _player1 = p1;
            _player2 = p2;
            _map = map;
            _turnCurrent = turn;
            _turnNumber = nbTurns;
            _playerTurn = playerTurn;
        }

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

        public void moveUnit()
        {
            throw new System.NotImplementedException();
        }
    }
}
