using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class SaveManagerXml : SaveManager, SmallWorld.ISaveManagerXml
    {
        public override Player[] Players { get; protected set; }
        public override Tuple<int, int, int> GMState { get; protected set; }
        public override Tuple<int, int, List<int>> Map { get; protected set; }
        public override void load()
        {
            throw new NotImplementedException();
        }

        public override void save()
        {
            throw new NotImplementedException();
        }
        public override void end()
        {
            throw new NotImplementedException();
        }
    }
}
