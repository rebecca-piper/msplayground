using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Scratch
{
    internal class Logic
    {
        Settings Settings = new Settings();
        Request Request = new Request();
        public int[] TicketDetails;
        Random rnd = new Random();
        public int Prize;
        public int BonusPrize;
        public void GetSymbols()
        {
            Settings.SetVersion();
            List<int> winsymbol = new List<int>();
            int winchance;
            int wincount =0;
            for (int i = Settings.Outcomes.Length; i > 0; i--)
            {
                winchance = rnd.Next(1, 10000000);
                if (winchance < Settings.Outcomes[i - 1])
                {
                   winsymbol.Add(i);
                   wincount++;
                }
                if (wincount >= 3)
                {
                   break;
                }
            }
            GettTicket(winsymbol);
        }

        public void GettTicket(List<int> winsymbol)
        {
            List<int> list = new List<int>();
            foreach (int symbol in winsymbol)
            {
                if(symbol != Settings.BonusSymbol)
                {
                    list.AddRange(Enumerable.Repeat(symbol, Settings.NumberToMatch));
                }
                else
                {
                    list.Add(symbol);
                }
            }
            TicketDetails = new int[9];
            list.CopyTo(TicketDetails);

            for (int j = 0; j < Settings.TicketSize; j++)
            {
                if (TicketDetails[j] == 0)
                    do
                    {
                    TicketDetails[j] = rnd.Next(1, 8);
                    }
                    while (TicketDetails.Count(s => s == TicketDetails[j]) > 2);
            }

            for (int i = 0; i < TicketDetails.Length; i++)
            {
                int numIndex = rnd.Next(TicketDetails.Length);
                int temp = TicketDetails[i];
                TicketDetails[i] = TicketDetails[numIndex];
                TicketDetails[numIndex] = temp;
            }
        }
        public void PlayGame()
        {
            decimal rtp = 0;
            decimal totalpaidprize = 0;
            decimal totalstake = 0;
            float rtpAgain = 0;
            int bonusgames = 1;
            int totalbonusprize = 0;
            float gameCount = 0;
            float multipliers = 0;

            Dictionary<int, long> MultiplierCounts = new Dictionary<int, long>();

            List<int> prizes = new List<int>();
            for (int i = 1; i <= 10000000; i++)
            {
                //Console.ReadLine();
                gameCount++;
                prizes = new List<int>();
                Request.GetRequest();
                GetSymbols();
                //Console.WriteLine(string.Join(",", TicketDetails));
                for (int matchedSymbol = 1; matchedSymbol <= Settings.NumberOfSymbols; matchedSymbol++)
                {
                    Prize = 0;
                    int count = TicketDetails.Count(x => x == matchedSymbol);
                    if (count >= Settings.NumberToMatch)
                    {
                        Prize = Settings.WinMultipliers[matchedSymbol - 1] * Request.Stake;
                        Request.Balance = Request.Balance + Prize;
                        multipliers += Settings.WinMultipliers[matchedSymbol - 1];
                    }
                    prizes.Add(Prize);
                }
                
                totalpaidprize += prizes.Sum();
                totalstake += Request.Stake;
                rtp = (totalpaidprize / totalstake) * 100;
                rtpAgain = multipliers / gameCount * 100;

                bool bonus = TicketDetails.Contains(Settings.BonusSymbol);
                if (bonus)
                {
                    bonusgames++;
                    int SpinOutcome = rnd.Next(0, Settings.BonusSymbolSize);
                    BonusPrize = Settings.BonusWinMultipliers[SpinOutcome] * Request.Stake;
                    totalbonusprize = totalbonusprize + BonusPrize;
                }
            }
            Console.WriteLine($"Ticket: {string.Join(",", TicketDetails)}");
            Console.WriteLine($"Total Prize: {totalpaidprize}");
            Console.WriteLine($"Total Stake: {totalstake}");
            Console.WriteLine($"Balance: {Request.Balance}");
            Console.WriteLine($"RTP: {rtp.ToString("F10")} or {rtpAgain.ToString("F10")}");
            Console.WriteLine($"Bonus Prize Average: {totalbonusprize / bonusgames}");

        }
    }

}
