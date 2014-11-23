﻿using System;
namespace SmallWorld
{
    interface ISaveManager
    {
        Tuple<int, int, int> getGameState();
        Tuple<int, int, System.Collections.Generic.List<int>> getMapData();
        Tuple<string, string, int> getPlayerData();
        System.Collections.Generic.IEnumerable<UnitData> getUnitsData();
        void load();
        void save();
    }
}