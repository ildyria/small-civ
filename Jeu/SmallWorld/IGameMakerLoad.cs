﻿using System;
namespace SmallWorld
{
    interface IGameMakerLoad
    {
        System.Collections.Generic.List<Unit> createUnits(int numPlayer);
        void init();
        GameManager makeGameManager(Player p1, Player p2, GameMap map);
        GameMap makeMap();
        Player makePlayer(int numPlayer);
        void end();
    }
}
