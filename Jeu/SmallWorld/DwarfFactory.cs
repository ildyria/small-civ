using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class DwarfFactory : UnitFactory, SmallWorld.IDwarfFactory
    {
        public override Unit makeUnit()
        {
            return new Dwarf(0, 0, 0, 2, 5, 2, "Random Dwarf", 0);
        }
    }
}
