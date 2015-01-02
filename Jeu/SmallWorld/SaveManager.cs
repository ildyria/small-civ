using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SmallWorld
{
    
    public abstract class SaveManager : SmallWorld.ISaveManager
    {
        private Tuple<int, int, int> _gmstate;
        private Tuple<int, int, List<int>> _map;

        public static readonly string saveFolder = Directory.GetCurrentDirectory() + "/Saves/";
        public string FileName { get; set; }
        public abstract Player[] Players { get; protected set; }
        public abstract Tuple<int, int, int> GMState { get; protected set; }
        public abstract Tuple<int, int, List<int>> Map { get; protected set; }
        public abstract void load();

        public abstract void save();

        public abstract void end();
     }
}
