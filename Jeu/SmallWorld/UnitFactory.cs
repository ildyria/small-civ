using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class UnitFactory : SmallWorld.IUnitFactory
    {
        public abstract Unit makeUnit(int posX = 0, int posY = 0, int movesLeft = 0, int armour = 0, int life = 0, int attack = 0, string name = "Random Unit", int value = 0);
    }
}
