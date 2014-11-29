using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmallWorld
{
    public abstract class Tile : SmallWorld.ITile
    {
        private Bitmap _image;

        private TerrainType _type;

        public Tile(Bitmap img, TerrainType type) 
        {
            _image = img;
            _type = type;
        }
        public TerrainType getType()
        {
            return _type;
        }

        public Bitmap getImage()
        {
            return _image;
        }

        public void setImage(Bitmap image)
        {
            _image = image;
        }
    }
}
