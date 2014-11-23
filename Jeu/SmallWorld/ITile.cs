using System;
namespace SmallWorld
{
    interface ITile
    {
        void getImage();
        TerrainType getType();
        void setImage();
    }
}
