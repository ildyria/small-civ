using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld
{
    public class Mountain : Tile, SmallWorld.IMountain
    {
        public Mountain() : base(new Bitmap("../textures/tile_mountain.png"), TerrainType.MOUNTAIN) { }
    }
}
