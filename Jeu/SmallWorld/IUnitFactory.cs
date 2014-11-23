using System;
namespace SmallWorld
{
    interface IUnitFactory
    {
        Unit makeUnit(int posX, int posY, int movesLeft, int armour, int life, int attack, string name, int value);
    }
}
