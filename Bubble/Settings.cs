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
            Outcomes = new int[] { 900, 2300, 1500, 400, 4600, 5250, 6857, 750000 };

            BonusSymbol = 8;
            BonusWinMultipliers = new int[] { 2, 4, 6, 8, 12, 16 };
            BonusSymbolSize = 6;
        }
    }
}