using System;
namespace SmallWorld
{
    interface IElf
    {
        void die();
        System.Collections.Generic.Dictionary<TerrainType, Tuple<int, int>> getTerrainData();
    }
}
