﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scratch
{
    internal class Settings
    {
        public int NumberOfSymbols;
        public int NumberToMatch;
        public int TicketSize;
        public int[] WinMultipliers;
        public int[] BonusWinMultipliers;
        public int[] Outcomes;
        public int BonusSymbol;
        public int BonusSymbolSize;
       
        public void SetVersion()
        {
            NumberOfSymbols = 8;
            NumberToMatch = 3;
            TicketSize = 9;
            WinMultipliers = new int[] {1, 2, 5, 10, 20, 50, 500, 0};
            Outcomes = new int[] { 2025100, 1024454, 100000, 400000, 38750, 7020, 200, 60000 };

            BonusSymbol = 8;
            BonusWinMultipliers = new int[] { 8, 32, 80, 240, 480, 1080};
            BonusSymbolSize = 6;
        }
    }
}
