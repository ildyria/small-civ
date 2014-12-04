using System;
namespace SmallWorld
{
    interface ITile
    {
        System.Windows.Media.Imaging.BitmapSource getImage();
        TerrainType getType();
        void setImage(System.Windows.Media.Imaging.BitmapSource image);
    }
}
