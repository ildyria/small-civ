using System;
namespace SmallWorld
{
    interface IClassicMap
    {
        System.Collections.Generic.List<int> generateMap();
        int NbTurnAdvised { get; }
        int NbUnitsAdvised { get; }
        Tuple<int, int> mapSize();
    }
}
