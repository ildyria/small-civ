using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class MapMaker : SmallWorld.IMapMaker
    {
        private Dictionary<TerrainType, Tile> _tiles;
        public MapMaker()
        {
            _tiles = new Dictionary<TerrainType, Tile>();
        }
        public Tile getTile(TerrainType type)
        {
            Tile t = null;  
            if (_tiles.TryGetValue(type, out t))
            {
                return t;
            }
            else // if the tile does not exist, we make it
            {
                // that code gave me cancer
                switch (type) {
                    case TerrainType.DESERT :
                        t = new Desert();
                        break;
                    case TerrainType.FOREST:
                        t = new Forest();
                        break;
                    case TerrainType.MOUNTAIN:
                        t = new Mountain();
                        break;
                    case TerrainType.PLAIN:
                        t = new Plain();
                        break;
                    default :
                        t = null;
                        break;
                }
                if (t != null){ //Exception, maybe ?
                    _tiles.Add(type, t);
                }
                return t;
            }
        }
    }
}
