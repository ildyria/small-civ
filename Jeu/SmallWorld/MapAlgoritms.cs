using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace SmallWorld
{
    public abstract class MapAlgoritms : SmallWorld.IMapAlgoritms
    {
        public abstract List<int> generateMap();
        public abstract Tuple<int, int> mapSize();
        public abstract List<Tuple<int, int>> getStartingPositions();
        public abstract int NbUnitsAdvised { get; }
        public abstract int NbTurnAdvised { get; }
        public abstract WrapperGenMap Generator { get; protected set; }
    }
}
