using System;
namespace SmallWorld
{
    interface IPlayer
    {
        void deleteUnit(Unit u);
        string getName();
        int getPoints();
        string getTribe();
        System.Collections.Generic.List<Unit> getUnits();
        void moveUnit(Unit u, int x, int y);
        void play();
        void scorePoints();
        void setUnits(System.Collections.Generic.List<Unit> unitList);
        System.Collections.Generic.List<Unit> unitsAt(int x, int y);
    }
}
