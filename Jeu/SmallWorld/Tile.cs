using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class Tile : SmallWorld.ITile
    {
        private int _image;

        private TerrainType _type;

        public Tile(int img, TerrainType type) 
        {
            _image = img;
            _type = type;
        }
        public TerrainType getType()
        {
            return _type;
        }

        public void getImage()
        {
            //return _image;
            throw new System.NotImplementedException();
        }

        public void setImage()
        {
            //_image = image
            throw new System.NotImplementedException();
        }
    }
}
