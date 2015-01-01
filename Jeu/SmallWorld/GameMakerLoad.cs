using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class GameMakerLoad : GameMaker, SmallWorld.IGameMakerLoad
    {
        // same name ? should be changed
        public SaveManager SaveManager { get; private set; }
        public GameMakerLoad()
        {
            //devrait-être dans init.
            SaveManager = new SaveManagerSerial();
        }

        public override void init()
        {
            SaveManager.load();
        }

        public override GameMap makeMap()
        {
            Tuple<int, int, List<int>> infos = SaveManager.getMapData();
            GameMap gm = new GameMap(infos.Item1, infos.Item2, infos.Item3);
            return gm;
        }

        public override Player makePlayer(int numPlayer)
        {
            /*Tuple<string, string, int> infos = SaveManager.getPlayerData();
            Player p = new Player(infos.Item1, infos.Item2, infos.Item3);
            p.UnitList = createUnits(numPlayer);
            return p;*/
            if (numPlayer > 0) 
            {
                return SaveManager.getPlayers()[numPlayer - 1];
            }
            else
            {
                return null;
            }
            
        }

        public override GameManager makeGameManager(Player p1, Player p2, GameMap map)
        {
            Tuple<int, int, int> state = SaveManager.getGameState();
            GameManager.init(p1, p2, map, state.Item1, state.Item2, state.Item3);
            GameManager.Instance.MapAlgo = new Wrapper.WrapperGenMap(map.SizeX, map.SizeY, map.TilesList);
            return GameManager.Instance;
        }

        public override List<Unit> createUnits(int numPlayer)
        {
            List<Unit> lu = SaveManager.getUnits(numPlayer);
            return lu;
        }
        public override void end()
        {
            SaveManager.end();
        }
    }
}
