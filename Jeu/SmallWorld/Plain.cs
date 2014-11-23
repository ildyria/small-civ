using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class Plain : Tile, SmallWorld.IPlain
    {
        public Plain() : base(1, TerrainType.PLAIN) { }
    }
}
