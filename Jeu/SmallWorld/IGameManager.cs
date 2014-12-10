using System;
using System.Collections.Generic;

namespace SmallWorld
{
    interface IGameManager
    {
        void computeFinalScore();
        bool gameEnd();
        void gameStart();
        GameMap Map { get; }
        int PlayerTurn { get; }
        int TurnNumber { get; }
        int TurnCurrent { get; }
        List<Unit> getAllUnits();
        void moveUnit(Unit u, int x, int y);
        void setPlayer1(Player p);
        void setPlayer2(Player p);
        Player getPlayer(int numPlayer);
    }
}
