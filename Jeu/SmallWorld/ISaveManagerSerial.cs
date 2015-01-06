using System;
using System.Collections.Generic;
namespace SmallWorld
{
    interface ISaveManagerSerial
    {
        string FileName { get; set; }
        Player[] Players { get; }
        Tuple<int, int, int> GMState { get; }
        Tuple<int, int, List<int>> Map { get; }
        void load();

        void save();
    }
}
