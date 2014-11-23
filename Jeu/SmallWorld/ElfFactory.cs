﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class ElfFactory : UnitFactory, SmallWorld.IElfFactory
    {
        public override Unit makeUnit(int posX = 0, int posY = 0, int movesLeft = 0, int armour = 1, int life = 5, int attack = 2, string name = "Random Elf", int value = 0)
        {
            return new Elf(posX, posY, movesLeft, armour, life, attack, name, value);
        }
    }
}
