using System;
using System.Windows.Media.Imaging;

namespace SmallWorld
{
    interface ITile
    {
        BitmapSource Image { get; }
        TerrainType TerrainType { get; }
    }
}
