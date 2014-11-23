using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class SaveManagerXml : SaveManager, SmallWorld.ISaveManagerXml
    {
        public override Tuple<int, int, List<int>> getMapData()
        {
            throw new System.NotImplementedException();
        }

        public override Tuple<string, string, int> getPlayerData()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<UnitData> getUnitsData(int numPlayer)
        {
            throw new System.NotImplementedException();
        }

        public override void load()
        {
            throw new System.NotImplementedException();
        }

        public override void save()
        {
            throw new System.NotImplementedException();
        }

        protected override void saveMap()
        {
            throw new System.NotImplementedException();
        }

        protected override void savePlayer()
        {
            throw new System.NotImplementedException();
        }

        protected override void saveUnit()
        {
            throw new System.NotImplementedException();
        }

        public override Tuple<int, int, int> getGameState()
        {
            throw new System.NotImplementedException();
        }
    }
}
