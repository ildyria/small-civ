using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class Desert : Tile, SmallWorld.IDesert
    {
        public Desert() : base(1, TerrainType.DESERT) { }
    }
}
