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
            int randNumber = rand.Next(0,101); // 101 because upper bound is not included
            if (randNumber > 50)
            {
                List<Tuple<int, int>> possible_move = new List<Tuple<int, int>>();
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        possible_move.Add(new Tuple<int, int>(X + i, Y + j));
                    }
                }

                if (Y % 2 == 0)
                {
                    possible_move.RemoveAt(8);
                    possible_move.RemoveAt(4);
                    possible_move.RemoveAt(2);
                }
                else
                {
                    possible_move.RemoveAt(6);
                    possible_move.RemoveAt(4);
                    possible_move.RemoveAt(0);
                }

                Tuple<int, int> pos = new Tuple<int, int>(0,0);
                int index = 0;
                while (index < possible_move.Count)
                {
                    pos = possible_move[index];
                    if (GameManager.Instance.opponent().unitsAt(pos.Item1, pos.Item2).Count != 0)
                    {
                        possible_move.RemoveAt(0);
                    }
                    else
                    {
                        index++;
                    }
                }

                if (possible_move.Count != 0)
                {
                    randNumber = rand.Next(0, possible_move.Count);  // count because upper bound is not included
                    pos = possible_move[randNumber];
                    setPosition(pos.Item1, pos.Item2);
                    Life++; // life is gained only if you can move
                }
            }
        }
    }
}
