using System;
using System.Collections.Generic;
namespace SmallWorld
{
    interface IGameMap
    {
        int SizeX { get; }
        int SizeY { get; }
        Tile getTile(int x, int y);
        List<int> TilesList { get; }
    }
}
