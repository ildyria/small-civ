using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace SmallWorld
{
    public class DemoMap : MapAlgoritms, SmallWorld.IDemoMap
    {
        static int _sideLength = 6;
        static int _nbUnit = 4;

        public override List<int> generateMap()
        {
            WrapperGenMap g = new WrapperGenMap(_sideLength, _sideLength);
            return g.generateMap(GameMap.nbTerrainType);
        }
        public override Tuple<int, int> mapSize()
        {
            return new Tuple<int, int>(_sideLength, _sideLength);
        }
        public override int nbUnits()
        {
            return _nbUnit;
        }
    }
}
