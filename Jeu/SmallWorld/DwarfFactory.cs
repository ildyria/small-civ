using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class DwarfFactory : UnitFactory, SmallWorld.IDwarfFactory
    {
        public override Unit makeUnit(int posX = 0, int posY = 0, int movesLeft = 0, int armour = 1, int life = 5, int attack = 2, string name = "Random Dwarf", int value = 0)
        {
            return new Dwarf(posX, posY, movesLeft, armour, life, attack, name, value);
        }
    }
}
