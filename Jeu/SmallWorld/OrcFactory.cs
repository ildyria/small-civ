using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class OrcFactory : UnitFactory, SmallWorld.IOrcFactory
    {
        public override Unit makeUnit()
        {
            return new Orc(0, 0, 0, 2, 5, 2, "Random Orc", 0);
        }

    }
}
