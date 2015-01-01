using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SmallWorld
{
    
    public abstract class SaveManager : SmallWorld.ISaveManager
    {
        public static readonly string saveFolder = Directory.GetCurrentDirectory() + "/Saves/";
        public string FileName { get; set; }
        public abstract void load();
        public abstract Tuple<string, string, int> getPlayerData();
        public abstract Tuple<int, int, List<int>> getMapData();
        public abstract void save();
        protected abstract void savePlayer();
        protected abstract void saveUnit();
        protected abstract void saveMap();
        public abstract List<Unit> getUnits(int numPlayer);
        public abstract Player[] getPlayers();

        public abstract Tuple<int, int, int>  getGameState();
        public abstract void end();
    }
}
