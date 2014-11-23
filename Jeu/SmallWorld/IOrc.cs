using System;
namespace SmallWorld
{
    interface IOrc
    {
        void fight(Unit opponent);
        System.Collections.Generic.Dictionary<TerrainType, Tuple<int, int>> getTerrainData();
    }
}
