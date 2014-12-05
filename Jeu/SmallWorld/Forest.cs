using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SmallWorld
{
    public class Forest : Tile, SmallWorld.IForest
    {
        public Forest() : base(new BitmapImage(new Uri("textures/tile_forest.png", UriKind.Relative)), TerrainType.FOREST) { }

        public override string toStringFR()
        {
            return "Forêt";
        }
    }
}
