﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Infrastructure
{
    public interface IQueueRepository
    {
        void Save(string message);
    }
}
