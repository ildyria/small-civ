using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class Dwarf : Unit, SmallWorld.IDwarf
    {
       
        protected static new readonly Dictionary<TerrainType, Tuple<int, int>> terrainData = new Dictionary<TerrainType, Tuple<int, int>>()
        {
            {TerrainType.PLAIN, new Tuple<int, int>(1, 0)}
        };
        public override Dictionary<TerrainType, Tuple<int, int>> getTerrainData()
        {
            return Dwarf.terrainData;
        }
        public Dwarf(int posX, int posY, int movesLeft, int armour, int life, int attack, string name, int value) : base(posX, posY, movesLeft, armour, life, attack, name, value) { }

        public override int moveCost(int x, int y, Tile t)
        {
            int res = base.moveCost(x, y, t);
            if (res != IMPOSSIBLE_MOVE)
            {
                return res;
            }
            else if (t.getType() == TerrainType.MOUNTAIN) // need to check that you are on a mountain and that no enemy is there
            {
                return Dwarf.DEFAULT_MOVE_COST;
            }
            else 
            {
                return 0;
            }
        }

    }
}
