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
using SmallWorld;

namespace UserInterface
{
    class BoardView : Canvas
    {
        static int TILESIZE = 60;
        private ContentControl _playerCursor;
        private int _i;
        private int _j;


        public BoardView() : base()
        {
            //_playerCursor = (ContentControl) this.Children[0];
            //Test Code
            /*GameMakerNew gmn = new GameMakerNew();
            gmn.setTribes(new UnitType[2] { UnitType.DWARF, UnitType.ORC });
            gmn.setNames(new string[2] { "J1", "J2" });
            gmn.setMapSize(MapSize.DEMO);
            gmn.makeGame();*/
            //GameManager.Instance().setMap(new GameMap(5, 5, new List<int> { 1, 2, 3, 0, 1, 1, 1, 1, 2, 3, 0, 1, 1, 1, 3, 3, 3, 2, 2, 2, 3, 3, 3, 2, 2 }));
        }
        private void mouseDown(object sender, MouseButtonEventArgs e)
        {
            double ch = 2 * TILESIZE / (Math.Sqrt(7) + 1);
            double a = (TILESIZE - ch) / 2;

            Point p = e.GetPosition(this);
            double x = p.X;
            double y = p.Y;

            _i = (int) ((TILESIZE - a) / y); ;
            double xOffset = (_i % 2 == 1) ? TILESIZE / 2 : 0;
            _j = (int) ((x - xOffset) / TILESIZE);

            double xR = xOffset + _j * TILESIZE;
            double yR = _i * (TILESIZE - a);
            //Canvas.SetLeft(_playerCursor, xR);
            //Canvas.SetTop(_playerCursor, yR);
        }
        

        protected override void OnRender(DrawingContext dc)
        {
            double ch = 2 * TILESIZE / (Math.Sqrt(7) + 1);
            double a = (TILESIZE - ch) / 2;
            double xOffset = 0, x = 0, y = 0;

            GameMap map = GameManager.Instance().getMap();
            if (map != null)
            {
                for (int i = 0; i < map.getSize().Item1; i++)
                {

                    xOffset = (i % 2 == 1) ? TILESIZE / 2 : 0;
                    for (int j = 0; j < map.getSize().Item2; j++)
                    {
                        x = xOffset + j * TILESIZE;
                        y = i * (TILESIZE - a);

                        IntPtr btptr = map.getTile(i, j).getImage().GetHbitmap();
                        ImageSource im = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(btptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        // Show tile at pos x,y
                        dc.DrawImage(im, new Rect(x, y, TILESIZE, TILESIZE));
                    }
                }
                //May be necessary
                //base.OnRender(dc);
            }
        }
    }
}
