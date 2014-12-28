using System;
using System.Collections.Generic;
namespace SmallWorld
{
    interface IPlayer
    {
        void deleteUnit(Unit u);
        string Name { get; }
        int Points { get; }
        string Tribe { get; }
        List<Unit> UnitList { get; set; }
        bool moveUnit(Unit u, int x, int y);
        void play();
        void scorePoints();
        List<Unit> unitsAt(int x, int y);
    }
}
