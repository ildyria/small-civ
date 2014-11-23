using System;
namespace SmallWorld
{
    interface IMapMaker
    {
        Tile getTile(TerrainType type);
    }
}
