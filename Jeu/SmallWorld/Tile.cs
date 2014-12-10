using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace SmallWorld
{
    public abstract class Tile : SmallWorld.ITile
    {
        public static readonly int TILESIZE = 60;

        public BitmapSource Image { get; private set; }
        public TerrainType TerrainType { get; private set; }

        public Tile(BitmapSource img, TerrainType type) 
        {
            Image = img;
            TerrainType = type;
        }

        public abstract string toStringFR();
    }
}
