﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaiterClient.Abstract
{
    public interface IMainWindowViewModel
    {
        bool LoginUser(string login, string password, out string error);
    }
}
