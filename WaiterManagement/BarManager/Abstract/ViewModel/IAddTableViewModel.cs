﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarManager.Abstract
{
    public interface IAddTableViewModel
    {
        bool AddTable(out string error);

        void Clear();
    }
}
