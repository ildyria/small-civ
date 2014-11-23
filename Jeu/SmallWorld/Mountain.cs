using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class Mountain : Tile, SmallWorld.IMountain
    {
        public Mountain() : base(1, TerrainType.MOUNTAIN) { }
    }
}
