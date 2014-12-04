using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace SmallWorld
{
    public class ClassicMap : MapAlgoritms, SmallWorld.IClassicMap
    {
        // Values could be directly written in functions ...
        static int _sideLength = 14;
        static int _nbUnitAdvised = 8;
        static int _nbTurnAdvised = 30;

        private List<Tuple<int, int>> _startingPos;

        public override List<int> generateMap()
        {
            WrapperGenMap g = new WrapperGenMap(_sideLength, _sideLength);
            List<int> gmap = g.generateMap(GameMap.nbTerrainType);
            _startingPos = g.placePlayer(new List<int>());
            return gmap;
        }
        public override List<Tuple<int, int>> getStartingPositions()
        {
            return _startingPos;
        }
        public override Tuple<int, int> mapSize()
        {
            return new Tuple<int, int>(_sideLength, _sideLength);
        }
        public override int getNbUnitsAdvised()
        {
            return _nbUnitAdvised;
        }
        public override int getNbTurnAdvised()
        {
            return _nbTurnAdvised;
        }
    }
}
