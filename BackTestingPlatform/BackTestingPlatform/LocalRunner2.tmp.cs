﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackTestingPlatform.Strategies;
using BackTestingPlatform.Strategies.MA;

namespace BackTestingPlatform
{
    class LocalRunner
    {
        public void run()
        {
            Strategy backTesting = new BackTesting();
            backTesting.act();
        }
    }
}