using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class ElfFactory : UnitFactory, SmallWorld.IElfFactory
    {
        public override Unit makeUnit()
        {
            return new Elf(0, 0, 0, 2, 5, 2, "Random Elf", 0);
        }
    }
}
