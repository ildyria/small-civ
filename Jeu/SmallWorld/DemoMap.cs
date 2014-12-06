﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace SmallWorld
{
    public class DemoMap : MapAlgoritms, SmallWorld.IDemoMap
    {
        // Values could be directly written in functions ...
        static int _sideLength = 6;
        static int _nbUnitAdvised = 4;
        static int _nbTurnAdvised = 5;

        private WrapperGenMap _g;

        public override List<int> generateMap()
        {
            _g = new WrapperGenMap(_sideLength, _sideLength);
            return _g.generateMap(GameMap.nbTerrainType);
        }
        public override List<Tuple<int, int>> getStartingPositions()
        {
            return _g.placePlayer(new List<int>());
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

        public override WrapperGenMap getGenerator()
        {
            return _g;
        }

    }
}
