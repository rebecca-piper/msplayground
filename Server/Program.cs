﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServerSetup serversetup = new ServerSetup();
            serversetup.ExecuteServer();
        }
    }
}