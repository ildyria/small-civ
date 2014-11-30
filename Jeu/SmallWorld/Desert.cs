using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld
{
    public class Desert : Tile, SmallWorld.IDesert
    {
        public Desert() : base(new Bitmap("textures/tile_desert.png"), TerrainType.DESERT) { }
    }
}
