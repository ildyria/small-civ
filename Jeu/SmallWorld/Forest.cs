using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class Forest : Tile, SmallWorld.IForest
    {
        public Forest() : base(2, TerrainType.FOREST) { }
    }
}
