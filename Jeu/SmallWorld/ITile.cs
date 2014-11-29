using System;
namespace SmallWorld
{
    interface ITile
    {
        System.Drawing.Bitmap getImage();
        TerrainType getType();
        void setImage(System.Drawing.Bitmap image);
    }
}
