using System;
namespace SmallWorld
{
    interface IGameManager
    {
        void computeFinalScore();
        bool gameEnd();
        void gameStart();
        GameMap getMap();
        Player getPlayer1();
        Player getPlayer2();
        int getPlayerTurn();
        int getTurnNumber();
        int getTurnCurrent();
        System.Collections.Generic.List<Unit> getUnits();
        void moveUnit();
        void setMap(GameMap map);
        void setPlayer1(Player p);
        void setPlayer2(Player p);
    }
}
