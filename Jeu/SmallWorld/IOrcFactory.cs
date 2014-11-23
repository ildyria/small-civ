using System;
namespace SmallWorld
{
    interface IOrcFactory
    {
        Unit makeUnit(int posX = 0, int posY = 0, int movesLeft = 0, int armour = 1, int life = 5, int attack = 2, string name = "Random Orc", int value = 0);
    }
}
