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
        public int[] BonusWinOutcomes;
        public int BonusSymbol;
        public int BonusSymbolSize;
        public int[][] Reels;
        public int[][] Winlines;
        public int WildSymbol;
        public void SetVersion()
        {
            NumberOfSymbols = 8;
            NumberToMatch = 3;
            TicketSize = 9;
            WinMultipliers = new int[] {1, 2, 5, 10, 20, 50, 500, 0};
            Outcomes = new int[] { 2025100, 1024454, 100000, 400000, 38750, 2400, 185, 30000};

            BonusSymbol = 8;
            BonusWinMultipliers = new int[] {6, 8, 10, 12, 12};
            //BonusWinOutcomes = new int[] {262500, 187500, 150000, 93750, 75000};
            BonusSymbolSize = 5;
            WildSymbol = 5;
            Reels = new[]
            {
                new [] {3, 1, 3, 2, 4, 5, 1, 2, 3, 2, 1, 4, 3, 5, 2, 4, 3, 1, 4, 5, 1, 3, 2, 1, 3, 3, 1},
                new [] {1, 2, 5, 3, 4, 3, 1, 2, 4, 2, 3, 1, 3, 5, 4, 5, 4, 1, 3, 2, 3, 1, 5, 3, 1, 1 ,2},
                new [] {4, 3, 1, 2, 3, 5, 1, 4, 3, 2, 3, 4, 1, 3, 2, 5, 3, 1, 4, 2, 1, 5, 2, 1, 3, 4, 3}
            };
            Winlines = new[]
            {
               new []{0,0,0},
               new []{1,1,1},
               new []{2,2,2},
               new []{0,1,2},
               new []{2,1,0}
            };
        }
    }
}
