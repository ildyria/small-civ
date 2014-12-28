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
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public List<int> TilesList { get; private set; }
        public MapMaker MapMaker { get; private set; }

        public GameMap(int sizeX, int sizeY, List<int> tiles)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            TilesList = tiles;
            MapMaker = new MapMaker();

        }
        public Tile getTile(int x, int y)
        {
            return this.getTile(x * SizeX + y);   
        }
        public Tile getTile(int z)
        {
            return MapMaker.getTile((TerrainType)TilesList[z]);
        }

        /*public List<int> getAdvisedTiles()
        {
            //_mapMaker.
        }*/
    }
}
