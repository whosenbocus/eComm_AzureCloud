﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Infrastructure
{
    public interface ITopic
    {
        void save(string message);
    }
}
