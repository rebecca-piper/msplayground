using System;
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
            WinMultipliers = new int[] {1, 2, 10, 20, 50, 500, 0};
            Outcomes = new int[] { 2025100, 1024454, 327500, 100000, 7020, 200, 50000 };

            BonusSymbol = 8;
            BonusWinMultipliers = new int[] { 4, 16, 40, 120, 240, 540};
            BonusSymbolSize = 6;
        }
    }
}
