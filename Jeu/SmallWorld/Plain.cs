using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SmallWorld
{
    public class Plain : Tile, SmallWorld.IPlain
    {
        public Plain() : base(new BitmapImage(new Uri("textures/tile_plain.png", UriKind.Relative)), TerrainType.PLAIN) { }
        //new Bitmap("tile_plain.png")
    }
}
