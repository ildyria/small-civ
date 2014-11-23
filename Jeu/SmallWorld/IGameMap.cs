using System;
namespace SmallWorld
{
    interface IGameMap
    {
        Tuple<int, int> getSize();
        Tile getTile(int x, int y);
        System.Collections.Generic.List<int> getTileList();
    }
}
