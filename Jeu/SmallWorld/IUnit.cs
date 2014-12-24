using System;
using System.Collections.Generic;
namespace SmallWorld
{
    interface IUnit
    {
        void damage();
        void die();
        List<Enum> fight(Unit opponent);
        Enum fightRound(Unit opponent);
        int Armour { get; }
        int Attack { get; }
        int Life { get; }
        int MovesLeft { get; }
        string Name { get; }
        int Value { get; }
        int X { get; }
        int Y { get; }
        System.Collections.Generic.Dictionary<TerrainType, Tuple<int, int>> getTerrainData();

        List<Enum> move(int x, int y, Tile t, System.Collections.Generic.IEnumerable<Unit> advList);
        int moveCost(int x, int y, Tile t);
        int scorePoints(Tile t);
        void setPosition(int x, int y);
        void startTurn();
        Tile whereAmI();
        int tileMoveCost(Tile t);
    }
}
