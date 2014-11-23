using System;
namespace SmallWorld
{
    interface ISaveManagerText
    {
        Tuple<int, int, int> getGameState();
        Tuple<int, int, System.Collections.Generic.List<int>> getMapData();
        Tuple<string, string, int> getPlayerData();
        System.Collections.Generic.List<Unit> getUnits(int numPlayer);
        void load();
        void save();
    }
}
