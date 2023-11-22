using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Scratch
{
    internal class Logic
    {
        Settings Settings = new Settings();
        Request Request = new Request();
        public List<int> TicketDetails;
        Random Rnd = new Random();
        public int Prize;
        public int BonusPrize;
        public Dictionary<int, int> Outcomes = new Dictionary<int,int>();
        int Counter = 0;
        
        public void dict()
        {
            for (int i = 1; i <= 8; i++)
            {
                Outcomes.Add(i, Counter);
            }
        }
        public void GetSymbols()
        {
            Settings.SetVersion();
            List<int> winsymbol = new List<int>();
            int winchance;
            int wincount =0;
            
           
            for (int i = Settings.Outcomes.Length; i > 0; i--)
            {
                winchance = Rnd.Next(1, 10000000);
                if (winchance < Settings.Outcomes[i - 1])
                {
                   winsymbol.Add(i);
                   wincount++;
                    Outcomes.TryGetValue(i, out Counter);
                    Outcomes[i] += 1;
                }
                if (wincount == 3)
                {
                   break;
                }
            }
            GetTicket(winsymbol);
        }

        public void GetTicket(List<int> winsymbol)
        {

            List<int> ticket = new List<int>();
            //losing symbol list
            //for each symbol if winSymbols doesn't contain symbol, add 2 of losing symbol to losing list
            //shuffle losing symbol list
            //add 3 symbols of winning symbols to ticket
            //fill the rest of the ticket with shuffled losing symbol list
            //shuffle the ticket
            for (int j = 1; j <= Settings.NumberOfSymbols - 1; j++)
            {
                if (!winsymbol.Contains(j))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        ticket.Add(j);
                    }
                }
            }
            
            foreach (int symbol in winsymbol.ToList())
            {
                if(symbol != Settings.BonusSymbol)
                {
                    winsymbol.AddRange(Enumerable.Repeat(symbol, Settings.NumberToMatch - 1));
                }
            }
            TicketDetails = new List<int>(winsymbol);
            Shuffle(ticket);

            for (int i = winsymbol.Count(); i < Settings.TicketSize; i++)
            {
                TicketDetails.Add(ticket[i]);
            }
            Shuffle(TicketDetails);
        }
        public void Shuffle(List<int> ticket)
        {
            for (int i = 0; i < ticket.Count; i++)
            {
                int numIndex = Rnd.Next(ticket.Count);
                int temp = ticket[i];
                ticket[i] = ticket[numIndex];
                ticket[numIndex] = temp;
            }
        }
        public void PlayGame()
        {
            decimal rtp = 0;
            decimal totalpaidprize = 0;
            decimal totalstake = 0;
            decimal rtpAgain = 0;
            int bonusgames = 1;
            int totalbonusprize = 0;
            decimal gameCount = 0;
            decimal multipliers = 0;
            List<int> prizes = new List<int>();
           
            for (int i = 1; i <= 1000000; i++)
            {
                //Console.ReadLine();
                
                prizes = new List<int>();
                Request.GetRequest();
                GetSymbols();
                for (int matchedSymbol = 1; matchedSymbol <= Settings.NumberOfSymbols; matchedSymbol++)
                {
                    gameCount++;
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
                    int SpinOutcome = Rnd.Next(0, Settings.BonusSymbolSize);
                    BonusPrize = Settings.BonusWinMultipliers[SpinOutcome] * Request.Stake;
                    totalbonusprize = totalbonusprize + BonusPrize;
                    totalpaidprize += BonusPrize;
                }
            }
            Console.WriteLine($"Ticket: {string.Join(",", TicketDetails)}");
            Console.WriteLine($"Total Prize: {totalpaidprize}");
            Console.WriteLine($"Total Stake: {totalstake}");
            Console.WriteLine($"Balance: {Request.Balance}");
            Console.WriteLine($"RTP: {rtp.ToString("F7")} or {rtpAgain.ToString("F5")}");
            Console.WriteLine($"Total outcomes: {gameCount}");
            foreach (var val in Outcomes.Values)
            {
                Console.WriteLine(val);
            }
        }
    }

}
