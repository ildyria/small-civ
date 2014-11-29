using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld
{
    public class Plain : Tile, SmallWorld.IPlain
    {
        public Plain() : base(new Bitmap("../textures/tile_plain.png"), TerrainType.PLAIN) { }
    }
}
