using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class SaveManager : SmallWorld.ISaveManager
    {
        public abstract void load();
        public abstract Tuple<string, string, int> getPlayerData();
        public abstract Tuple<int, int, List<int>> getMapData();
        public abstract void save();
        protected abstract void savePlayer();
        protected abstract void saveUnit();
        protected abstract void saveMap();
        public abstract IEnumerable<UnitData> getUnitsData(int numPlayer);

        public abstract Tuple<int, int, int>  getGameState();
    }
}
