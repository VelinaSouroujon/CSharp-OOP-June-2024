﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Interfaces.Factories
{
    public interface ILayoutFactory
    {
        ILayout CreateLayout();
    }
}
