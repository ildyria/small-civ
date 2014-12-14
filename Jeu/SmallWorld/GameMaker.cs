using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    // Should not be here
    public enum UnitType { DWARF, ELF, ORC};
    public enum MapSize { DEMO, SMALL, CLASSIC};
    public abstract class GameMaker : SmallWorld.IGameMaker
    {
        public GameManager makeGame() {
            init();
            GameMap map = makeMap();
            Player p1 = makePlayer(1);
            Player p2 = makePlayer(2);
            GameManager gm = makeGameManager(p1, p2, map);
            end();
            return gm;
        }

        public abstract Player makePlayer(int numPlayer);
        public abstract GameMap makeMap();
        public abstract GameManager makeGameManager(Player p1, Player p2, GameMap map);
        public abstract void init();
        public abstract List<Unit> createUnits(int numPlayer);
        public abstract void end();
    }
}
