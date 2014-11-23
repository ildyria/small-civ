using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class Elf : Unit, SmallWorld.IElf
    {
        protected static new readonly Dictionary<TerrainType, Tuple<int, int>> terrainData = new Dictionary<TerrainType, Tuple<int, int>>()
        {
            {TerrainType.DESERT, new Tuple<int, int>(4, 1)},
            {TerrainType.FOREST, new Tuple<int, int>(1, 1)}
        };
        public override Dictionary<TerrainType, Tuple<int, int>> getTerrainData()
        {
            return Elf.terrainData;
        }
        public Elf(int posX, int posY, int movesLeft, int armour, int life, int attack, string name, int value) : base(posX, posY, movesLeft, armour, life, attack, name, value) {}
        public override void die()
        {
            Random rand = new Random();
            int randNumber = rand.Next(0,101);
            if (randNumber > 50)
            {
                //Should try to move
                bool canMove = true;
                if (canMove)
                {
                    _life++;
                    //setPosition(x, y);
                }
            }
            
        }
    }
}
