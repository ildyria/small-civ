using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld
{
    public class Forest : Tile, SmallWorld.IForest
    {
        public Forest() : base(new Bitmap("textures/tile_forest.png"), TerrainType.FOREST) { }
    }
}
