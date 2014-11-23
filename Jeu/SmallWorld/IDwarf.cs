using System;
namespace SmallWorld
{
    interface IDwarf
    {
        System.Collections.Generic.Dictionary<TerrainType, Tuple<int, int>> getTerrainData();
    }
}
