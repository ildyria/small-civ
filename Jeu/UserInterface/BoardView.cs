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
        public static int TILESIZE = 60;


        public BoardView() : base()
        {
           // _playerCursor = new ContentControl();
           // _playerCursor.Style = "{StaticResource cursorHex}";
  
            //_playerCursor = 
            //Test Code
            /*GameMakerNew gmn = new GameMakerNew();
            gmn.setTribes(new UnitType[2] { UnitType.DWARF, UnitType.ORC });
            gmn.setNames(new string[2] { "J1", "J2" });
            gmn.setMapSize(MapSize.DEMO);
            gmn.makeGame();*/
            //GameManager.Instance().setMap(new GameMap(5, 5, new List<int> { 1, 2, 3, 0, 1, 1, 1, 1, 2, 3, 0, 1, 1, 1, 3, 3, 3, 2, 2, 2, 3, 3, 3, 2, 2 }));
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
                    for (int j = 0; j < map.getSize().Item2; j++)
                    {
                        xOffset = (j % 2 == 1) ? TILESIZE / 2 : 0;
                        x = xOffset + i * TILESIZE;
                        y = j * (TILESIZE - a);

                        IntPtr btptr = map.getTile(i, j).getImage().GetHbitmap();
                        ImageSource im = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(btptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        // Show tile at pos x,y
                        dc.DrawImage(im, new Rect(x, y, TILESIZE, TILESIZE));
                    }

                }
                
                foreach (SmallWorld.Unit u in GameManager.Instance().getUnits())
                {
                    System.Diagnostics.Trace.WriteLine("hello");
                    dc.DrawRectangle(Brushes.LightGreen, new Pen(Brushes.White, 2), new Rect(u.getX(), u.getY(), 50, 50));
                    // this.Children.Add(XXX)
                }
            }
            //May be necessary
            //base.OnRender(dc);
        }
    }
}
