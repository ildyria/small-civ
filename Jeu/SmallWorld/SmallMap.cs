using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace SmallWorld
{
    public class SmallMap : MapAlgoritms, SmallWorld.ISmallMap
    {
        // Values could be directly written in functions ...
        static int _sideLength = 10;
        static int _nbUnitAdvised = 6;
        static int _nbTurnAdvised = 20;

        public override List<int> generateMap()
        {
            WrapperGenMap g = new WrapperGenMap(_sideLength, _sideLength);
            return g.generateMap(GameMap.nbTerrainType);
        }
        public override Tuple<int, int> mapSize() {
            return new Tuple<int, int>(_sideLength, _sideLength);
        }
        public override int getNbUnitsAdvised() {
            return _nbUnitAdvised;
        }
        public override int getNbTurnAdvised()
        {
            return _nbTurnAdvised;
        }
    }
}
