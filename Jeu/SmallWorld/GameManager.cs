using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace SmallWorld
{
    public class GameManager : SmallWorld.IGameManager
    {
        //Could also have been an argument of every function. Which is better ? 
        //A good way to decide would be to do that Rutger Hauer style but I don't want to declaim poetry on a rainy night on a rooftop with only underwear on.
        //Sorry if that seems a bit egoistic.
        private static GameManager _instance;

        private Player[] _players;
        public int TurnCurrent { get; private set; }
        public int TurnNumber { get; private set; }
        public int PlayerTurn { get; private set; }
        public WrapperGenMap MapAlgo { get; set; }
        public GameMap Map { get; private set; }
        


        private GameManager() { }
        public static void  init(Player p1, Player p2, GameMap map, int nbTurns, int turn, int playerTurn)
        {
            _instance = new GameManager();
            _instance._players = new Player[2] {p1, p2};
            _instance.Map = map;
            _instance.TurnCurrent = turn;
            _instance.TurnNumber = nbTurns;
            _instance.PlayerTurn = playerTurn;
        }

        public static GameManager Instance() {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }

        //The following is not static. It should. But i have no chocolate, so i can't change that.      
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
        public void setPlayer2(Player p)
        {
            _players[1] = p;
        }

        public bool gameEnd()
        {
            if (TurnCurrent > TurnNumber || _players[0].getUnits().Count == 0 || _players[1].getUnits().Count == 0)
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
            return _players[PlayerTurn];
        }
        public void moveUnit(Unit u, int x, int y)
        {
            // You can only move units during your turn
            if (getCurrentPlayer().getUnits().Contains(u))
            {
                Tile start = Map.getTile(u.X, u.Y);
                Tile end = Map.getTile(x, y);
                List<Unit> opponents = _players[(PlayerTurn + 1) % 2].unitsAt(x, y);
                Player adv = _players[(PlayerTurn + 1) % 2];

                u.move(x, y, end, opponents);

                if (u.Life == 0)
                {
                    getCurrentPlayer().deleteUnit(u);
                }
                //another if, and not else if for compat with future updates.
                Unit op = opponents.Find(z => z.Life == 0);
                if (op != null) // does this work ?
                {
                    adv.deleteUnit(op);
                }
            }
        }
        public Player opponent()
        {
            return getPlayer((PlayerTurn + 1) % 2);
        }

        public void nextTurn()
        {
            _players[PlayerTurn].scorePoints();
            PlayerTurn = (PlayerTurn + 1) % 2;
            if (PlayerTurn  == 0)
            {
                TurnCurrent++;
            }
            // no need to reset movement to zero
            _players[PlayerTurn].play();
        }
    }
}
