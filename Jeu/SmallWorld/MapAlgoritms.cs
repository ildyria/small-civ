using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class MapAlgoritms : SmallWorld.IMapAlgoritms
    {
        public abstract List<int> generateMap();
        public abstract Tuple<int, int> mapSize();
        public abstract List<Tuple<int, int>> getStartingPositions();
        public abstract int getNbUnitsAdvised();
        public abstract int getNbTurnAdvised();
    }
}
