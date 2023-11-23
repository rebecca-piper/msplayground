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
        List<int> BonusWinSymbol = new List<int>();
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
           
            for (int i = 1; i <= 10000000; i++)
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
                    BonusWinSymbol = new List<int>();
                    bonusgames++;
                    
                    int[] reel1 = new int[3];
                    int[] reel2 = new int[3]; 
                    int[] reel3 = new int[3];
                    int[] winline = new int[3] { 0, 1, 2 };
                    int[][] reels = new int[3][] {reel1, reel2, reel3};
                    int wincount = 0;
                    for (int j = Settings.BonusWinOutcomes.Length; j > 0; j--)
                    {
                        int winchance = Rnd.Next(1, 700000);
                        if (winchance < Settings.BonusWinOutcomes[j - 1])
                        {
                            BonusWinSymbol.Add(j);
                            wincount++;
                            //Outcomes.TryGetValue(i, out Counter);
                            //Outcomes[i] += 1;
                        }
                        if (wincount == 1)
                        {
                            break;
                        }
                    }
                    if (wincount == 1)
                    {
                        foreach (int symbol in BonusWinSymbol)
                        {                          
                            for (int m = 0; m < reels.Length; m++)
                            {
                                reels[m][winline[m]] = symbol;
                            }
  
                        }
                        BonusPrize = Settings.BonusWinMultipliers[BonusWinSymbol[0] - 1] * Request.Stake;
                    }
                    FillReel(reels);
 
                    
                    Console.WriteLine($"{string.Join(",", reel1[0], reel2[0], reel3[0])}");
                    Console.WriteLine($"{string.Join(",", reel1[1], reel2[1], reel3[1])}");
                    Console.WriteLine($"{string.Join(",", reel1[2], reel2[2], reel3[2])}");

                    //int SpinOutcome = Rnd.Next(0, Settings.BonusSymbolSize);
                    //BonusPrize = Settings.BonusWinMultipliers[SpinOutcome] * Request.Stake;
                    totalbonusprize = totalbonusprize + BonusPrize;
                    totalpaidprize += BonusPrize;
                }
            }
            Console.WriteLine($"Ticket: {string.Join(",", TicketDetails)}");
            Console.WriteLine($"Total Prize: {totalpaidprize}");
            Console.WriteLine($"Total Stake: {totalstake}");
            Console.WriteLine($"Balance: {Request.Balance}");
            Console.WriteLine($"RTP: {rtp.ToString("F10")} or {rtpAgain.ToString("F5")}");
            Console.WriteLine($"Bonus prize av: {totalbonusprize / bonusgames}");
            foreach (var val in Outcomes.Values)
            {
                Console.WriteLine(val);
            }
            
        }
        public void FillReel(int[][] reels)
        {
            
            List<int> bonusSymbols = new List<int>();
            for (int j = 1; j <= Settings.BonusSymbolSize; j++)
            {
                  for (int i = 0; i < 2; i++)
                  {
                       bonusSymbols.Add(j);
                  }
            }
            Shuffle(bonusSymbols);
            for (int i =0; i < reels.Length; i++)
            {
                if (reels[i][i] == 0)
                {
                    reels[i][i] = bonusSymbols[i];
                }                 
            }
        }
    }

}
