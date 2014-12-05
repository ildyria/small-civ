using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SmallWorld
{
    public class Desert : Tile, SmallWorld.IDesert
    {
        public Desert() : base(new BitmapImage(new Uri("textures/tile_desert.png", UriKind.Relative)), TerrainType.DESERT) { }

        public override string toStringFR()
        {
            return "Désert";
        }
    }
}
