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
        private BitmapSource _image;
        private TerrainType _type;

        public Tile(BitmapSource img, TerrainType type) 
        {
            _image = img;
            _type = type;
        }
        public TerrainType getType()
        {
            return _type;
        }

        public BitmapSource getImage()
        {
            return _image;
        }

        public void setImage(BitmapSource image)
        {
            _image = image;
        }
    }
}
