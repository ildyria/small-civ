using System;
namespace SmallWorld
{
    interface IMapAlgoritms
    {
        System.Collections.Generic.List<int> generateMap();
        int getNbTurnAdvised();
        int getNbUnitsAdvised();
        Tuple<int, int> mapSize();
    }
}
