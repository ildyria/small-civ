using System;
using System.Collections.Generic;
namespace SmallWorld
{
    interface IOrc
    {
        List<Enum> fight(Unit opponent);
        Dictionary<TerrainType, Tuple<int, int>> getTerrainData();
    }
}
