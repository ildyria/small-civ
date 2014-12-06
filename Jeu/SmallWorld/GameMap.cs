using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace SmallWorld
{
    public enum TerrainType { DESERT, FOREST, MOUNTAIN, PLAIN };
    public class GameMap : SmallWorld.IGameMap
    {
        public static readonly int nbTerrainType = 4;
        private int _sizeX;
        private int _sizeY;
        private List<int> _tilesList;
        private MapMaker _mapMaker;

        public GameMap(int sizeX, int sizeY, List<int> tiles)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            _tilesList = tiles;
            _mapMaker = new MapMaker();


        }
        public Tile getTile(int x, int y)
        {
            return _mapMaker.getTile( (TerrainType) _tilesList[x *_sizeX + y]);   
        }

        public Tuple<int, int> getSize()
        {
            return new Tuple<int,int>(_sizeX, _sizeY);
        }

        public List<int> getTileList()
        {
            return _tilesList;
        }

        /*public List<int> getAdvisedTiles()
        {
            //_mapMaker.
        }*/
    }
}
