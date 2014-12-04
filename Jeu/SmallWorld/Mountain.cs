using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SmallWorld
{
    public class Mountain : Tile, SmallWorld.IMountain
    {
        public Mountain() : base(new BitmapImage(new Uri("textures/tile_mountain.png", UriKind.Relative)), TerrainType.MOUNTAIN) { }
    }
}
