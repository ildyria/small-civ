﻿using System;
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

        public override WrapperGenMap Generator { get; protected set; }

        public override List<int> generateMap()
        {
            Generator = new WrapperGenMap(_sideLength, _sideLength);
            return Generator.generateMap(GameMap.nbTerrainType);
        }
        public override List<Tuple<int, int>> getStartingPositions()
        {
            return Generator.placePlayer(new List<int>());
        }
        public override Tuple<int, int> mapSize()
        {
            return new Tuple<int, int>(_sideLength, _sideLength);
        }
        public override int NbUnitsAdvised
        {
            get { return _nbUnitAdvised; }
        }
        public override int NbTurnAdvised
        {
            get { return _nbTurnAdvised; }
        }
        
    }
}
