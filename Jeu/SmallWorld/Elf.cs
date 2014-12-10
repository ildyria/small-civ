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

                    int i = 0;
                    
                    List<Tuple<int, int>> possible_move = new List<Tuple<int, int>>();
                    for (i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            possible_move.Add(new Tuple<int, int>(_posX + i, _posY + j));
                        }
                    }

                    if (_posY%2 == 0)
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
                    i = 0;
                    while (i < possible_move.Count)
                    {
                        pos = possible_move[i];
                        if (GameManager.Instance().opponent().unitsAt(pos.Item1, pos.Item2).Count != 0)
                        {
                            possible_move.RemoveAt(0);
                        }
                        else
                        {
                            i++;
                        }
                    }

                    if (possible_move.Count != 0)
                    {
                        randNumber = rand.Next(0, possible_move.Count - 1);
                        pos = possible_move[randNumber];
                        setPosition(pos.Item1, pos.Item2);
                    }
                }
            }
            
        }
    }
}
