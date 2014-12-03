using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class GameMakerNew : GameMaker, SmallWorld.IGameMakerNew
    {
        private MapAlgoritms _mapGen;
        private UnitType[] _tribes;
        private string[] _names;
        private int[] _nbUnits;

        public GameMakerNew()
        {
            _nbUnits = null;
        }
        public override GameMap makeMap()
        {
            Tuple<int, int> xy = _mapGen.mapSize();
            List<int> tileList = _mapGen.generateMap();
            return new GameMap(xy.Item1, xy.Item2, tileList);
        }

        public override Player makePlayer(int numPlayer)
        {
            Player p = new Player(_names[numPlayer-1], tribeName(_tribes[numPlayer-1]), 0);
            
            p.setUnits(createUnits(numPlayer));
            return p;
        }

        public override GameManager makeGameManager(Player p1, Player p2, GameMap map)
        {
            // nbTurns = 10 ?
            GameManager.init(p1, p2, map, 10, 0, 0);
            return GameManager.Instance();
        }

        public override void init() {}

        public override List<Unit> createUnits(int numPlayer)
        {
            int nbUnits = _mapGen.getNbUnitsAdvised();
            if (_nbUnits != null)
            {
                nbUnits = _nbUnits[numPlayer - 1];
            }
            //get position of player X
            int x = 0, y = 0;
            List<Unit> ul = new List<Unit>();
            UnitFactory uf = tribeFactory(_tribes[numPlayer-1]);
            for (int i = 0; i < nbUnits; i++)
            {
                Unit u = uf.makeUnit();
                u.setPosition(x, y);
                ul.Add(u);
            }
            return ul;
        }

        public string tribeName(UnitType t)
        {
            switch(t) {
                case UnitType.DWARF :
                    return "Dwarf";
                case UnitType.ELF:
                    return "Elf";
                case UnitType.ORC:
                    return "Orc";
                default:
                    return "Unknown";
                    //Or null. Dunno. I like unknown.
            }
        }
        public UnitFactory tribeFactory(UnitType t)
        {
            switch (t)
            {
                case UnitType.DWARF:
                    return new DwarfFactory();
                case UnitType.ELF:
                    return new ElfFactory();
                case UnitType.ORC:
                    return new OrcFactory();
                default:
                    // It will throw an exception one day. Maybe. I'm not sure. I need chocolate to do that, and at the moment i have none. So no exception.
                    return null;
            }
        }

        public void setTribes(UnitType[] t)
        {
            _tribes = t;
        }
        public void setNames(string[] t)
        {
            _names = t;
        }
        public void setMapSize(MapSize size)
        {
            switch (size)
            {
                case MapSize.DEMO:
                    _mapGen = new DemoMap();
                    break;
                case MapSize.SMALL:
                    _mapGen = new SmallMap();
                    break;
                case MapSize.CLASSIC:
                    _mapGen = new ClassicMap();
                    break;
                default:
                    _mapGen = new DemoMap();
                    break;
            } 
        }
        public void setNbUnit(int[] nbunits)
        {
            _nbUnits = nbunits;
        }
    }
}
