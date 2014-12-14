using System;
namespace SmallWorld
{
    interface IGameMaker
    {
        System.Collections.Generic.List<Unit> createUnits(int numPlayer);
        void init();
        GameManager makeGame();
        GameManager makeGameManager(Player p1, Player p2, GameMap map);
        GameMap makeMap();
        Player makePlayer(int numPlayer);
        void end();
    }
}
