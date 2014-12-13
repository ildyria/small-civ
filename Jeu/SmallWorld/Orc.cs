using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    [Serializable]
    public class Orc : Unit, SmallWorld.IOrc
    {
        protected static new readonly Dictionary<TerrainType, Tuple<int, int>> terrainData = new Dictionary<TerrainType, Tuple<int, int>>()
        {
            {TerrainType.PLAIN, new Tuple<int, int>(1, 1)},
            {TerrainType.FOREST, new Tuple<int, int>(2, 0)}
        };
        public override Dictionary<TerrainType, Tuple<int, int>> getTerrainData()
        {
            return Orc.terrainData;
        }
        public Orc(int posX, int posY, int movesLeft, int armour, int life, int attack, string name, int value) : base(posX, posY, movesLeft, armour, life, attack, name, value) {}
        public override void fight(Unit opponent)
        {
            base.fight(opponent);
            // if the opponent is dead and i'm not
            if (opponent.Life == 0 && Life != 0)
            {
                Value++;
            }
        }
    }
}
