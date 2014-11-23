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

        /*public override bool movePossible(int x, int y, Tile t)
        {
            // faire attention : case montagne a coté avec adv = droit de se déplacer
            string type = t.getType();
            if (type == "Plain")
            {
                return _movesLeft >= 2 && base.movePossible(x, y, t);
            }
            else if (type == "Mountain")
            {
                // ne doit pas être permis si il y a une unité adverse
                return true;
            }
            else 
            {
                return base.movePossible(x, y, t);
            }
        }*/

    }
}
