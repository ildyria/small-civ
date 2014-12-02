using System;
namespace SmallWorld
{
    interface IDemoMap
    {
        System.Collections.Generic.List<int> generateMap();
        int getNbTurnAdvised();
        int getNbUnitsAdvised();
        Tuple<int, int> mapSize();
    }
}
