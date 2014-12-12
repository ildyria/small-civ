using System;
namespace SmallWorld
{
    interface IMapAlgoritms
    {
        System.Collections.Generic.List<int> generateMap();
        int NbTurnAdvised { get; }
        int NbUnitsAdvised { get; }
        Tuple<int, int> mapSize();
    }
}
