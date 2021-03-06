﻿using System;
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
    class BoardView : Panel
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
            //GameManager.Instance.setMap(new GameMap(5, 5, new List<int> { 1, 2, 3, 0, 1, 1, 1, 1, 2, 3, 0, 1, 1, 1, 3, 3, 3, 2, 2, 2, 3, 3, 3, 2, 2 }));
            //TILESIZE = (int) (App.Current.MainWindow.Width - 200) / 10;
        }
        
        protected override void OnRender(DrawingContext dc)
        {
            Tuple<double, double> mapSize = indexToCoord(GameManager.Instance.Map.SizeX, GameManager.Instance.Map.SizeY + 1);
            Width = mapSize.Item1;
            Height = mapSize.Item2;
            double ch = 2 * TILESIZE / (Math.Sqrt(7) + 1);
            double a = (TILESIZE - ch) / 2;
            double xOffset = 0, x = 0, y = 0;

            GameMap map = GameManager.Instance.Map;
            if (map != null)
            {
                for (int i = 0; i < map.SizeX; i++)
                {
                    for (int j = 0; j < map.SizeY; j++)
                    {
                        xOffset = (j % 2 == 1) ? TILESIZE / 2 : 0;
                        x = xOffset + i * TILESIZE;
                        y = j * (TILESIZE - a);

                        // Show tile at pos x,y
                        dc.DrawImage(map.getTile(i, j).Image, new Rect(x, y, TILESIZE, TILESIZE));
                    }

                }
            }
            //May be necessary
            //base.OnRender(dc);
        }
        public static Tuple<double, double> indexToCoord(int i, int j)
        {
            double ch = 2 * BoardView.TILESIZE / (Math.Sqrt(7) + 1);
            double a = (BoardView.TILESIZE - ch) / 2;
            double xOffset = getXOffset(j);

            double xR = xOffset + i * BoardView.TILESIZE;
            double yR = j * (BoardView.TILESIZE - a);
            return new Tuple<double, double>(xR, yR);
        }

        public static Tuple<int, int> coordToIndex(double x, double y)
        {
            double ch = 2 * BoardView.TILESIZE / (Math.Sqrt(7) + 1);
            double a = (BoardView.TILESIZE - ch) / 2;

            int j = (int)(y / (BoardView.TILESIZE - a));
            double xOffset = getXOffset(j);
            int i = (int)((x - xOffset) / BoardView.TILESIZE);
            return new Tuple<int, int>(i, j);
        }

        public static int getXOffset(int j)
        {
            return (j % 2 == 1) ? BoardView.TILESIZE / 2 : 0;
        }

    }
}
