using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class GameMakerNew : GameMaker, SmallWorld.IGameMakerNew
    {
        public MapAlgoritms MapGenerator { get; set; }
        public UnitType[] Tribes { get; set; }
        public string[] Names { get; set; }
        public int[] NbUnits { get; set; }
        private List<Tuple<int, int>> StartPositions { get; set; }

        public GameMakerNew()
        {
            NbUnits = null;
        }
        public override GameMap makeMap()
        {
            Tuple<int, int> xy = MapGenerator.mapSize();
            List<int> tileList = MapGenerator.generateMap();
            StartPositions = MapGenerator.getStartingPositions();
            return new GameMap(xy.Item1, xy.Item2, tileList);
        }

        public override Player makePlayer(int numPlayer)
        {
            Player p = new Player(Names[numPlayer-1], tribeName(Tribes[numPlayer-1]), 0);
            
            p.UnitList = createUnits(numPlayer);
            return p;
        }

        public override GameManager makeGameManager(Player p1, Player p2, GameMap map)
        {
            // nbTurns = 10 ?
            GameManager.init(p1, p2, map, MapGenerator.NbTurnAdvised, 1, 0);
            GameManager.Instance.MapAlgo = MapGenerator.Generator;
            return GameManager.Instance;
        }

        public override void init() {}

        public override List<Unit> createUnits(int numPlayer)
        {
            int nbUnits = MapGenerator.NbUnitsAdvised;
            if (NbUnits != null)
            {
                nbUnits = NbUnits[numPlayer - 1];
            }
            List<Unit> ul = new List<Unit>();
            UnitFactory uf = getTribeFactory(Tribes[numPlayer-1]);
            for (int i = 0; i < nbUnits; i++)
            {
                Unit u = uf.makeUnit();
                u.setPosition(StartPositions[numPlayer - 1].Item1, StartPositions[numPlayer - 1].Item2);
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
        public UnitFactory getTribeFactory(UnitType t)
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

        public void setMapSize(MapSize size)
        {
            switch (size)
            {
                case MapSize.DEMO:
                    MapGenerator = new DemoMap();
                    break;
                case MapSize.SMALL:
                    MapGenerator = new SmallMap();
                    break;
                case MapSize.CLASSIC:
                    MapGenerator = new ClassicMap();
                    break;
                default:
                    MapGenerator = new DemoMap();
                    break;
            } 
        }

        public override void end()
        {

        }
    }
}
