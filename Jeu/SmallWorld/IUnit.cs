using System;
namespace SmallWorld
{
    interface IUnit
    {
        void damage();
        void die();
        void fight(Unit opponent);
        bool fightRound(Unit opponent);
        int getArmour();
        int getAttack();
        int getLife();
        int getMovesLeft();
        string getName();
        System.Collections.Generic.Dictionary<TerrainType, Tuple<int, int>> getTerrainData();
        int getValue();
        int getX();
        int getY();
        void move(int x, int y, Tile t, System.Collections.Generic.IEnumerable<Unit> advList);
        int moveCost(int x, int y, Tile t);
        int scorePoints(Tile t);
        void setPosition(int x, int y);
        void startTurn();
    }
}
