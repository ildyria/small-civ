using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserInterface
{
    class GameView : Canvas
    {
        public GameView() : base() { }
        protected override void OnRender(DrawingContext dc)
        {
            double cc = 34;
            double ch = 2 * cc / (Math.Sqrt(7) + 1);
            double a = (cc - ch) / 2;

            double xOffset = 0, x = 0, y = 0;
            /*for (int i = 0; i < _gManager.getMap().getSize().Item1; i++)
            {

                xOffset = (i % 2 == 1) ? cc / 2 : 0;
                for (int j = 0; j < _gManager.getMap().getSize().Item2; j++)
                {
                    x = xOffset + j * cc;
                    y = i * (cc - a);
                    ImageSource im = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(_gManager.getMap().getTile(i, j).getImage().GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    _gManager.getMap().getTile(i, j).getImage();
                    // Show tile at pos x,y
                    dc.DrawImage(im, new Rect(x, y, TILESIZE, TILESIZE));
                }
            }*/
            Visibility = Visibility.Visible;
            base.OnRender(dc);
        }
    }
}
