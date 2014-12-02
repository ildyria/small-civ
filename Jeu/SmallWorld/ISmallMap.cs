using System;
namespace SmallWorld
{
    interface ISmallMap
    {
        System.Collections.Generic.List<int> generateMap();
        int getNbTurnAdvised();
        int getNbUnitsAdvised();
        Tuple<int, int> mapSize();
    }
}
