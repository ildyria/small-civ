using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class GameMakerLoad : GameMaker, SmallWorld.IGameMakerLoad
    {
        SaveManager sm;
        public GameMakerLoad()
        {
            //devrait-être dans init.
            sm = new SaveManagerXml();
        }
        public override GameMap makeMap()
        {
            Tuple<int, int, List<int>> infos = sm.getMapData();
            return new GameMap(infos.Item1, infos.Item2, infos.Item3);
        }

        public override Player makePlayer(int numPlayer)
        {
            Tuple<string, string, int> infos = sm.getPlayerData();
            Player p = new Player(infos.Item1, infos.Item2, infos.Item3);
            p.setUnits(createUnits(numPlayer));
            return p;
        }

        public override GameManager makeGameManager(Player p1, Player p2, GameMap map)
        {
            Tuple<int, int, int> state =  sm.getGameState();
            return new GameManager(p1, p2, map, state.Item1, state.Item2, state.Item3);
        }

        public override void init()
        {
            sm.load();
        }

        public override List<Unit> createUnits(int numPlayer)
        {
            throw new System.NotImplementedException();
        }
    }
}
