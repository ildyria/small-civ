﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public abstract class UnitFactory : SmallWorld.IUnitFactory
    {
        public abstract Unit makeUnit();
    }
}
