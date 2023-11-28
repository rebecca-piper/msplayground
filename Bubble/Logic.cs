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
        public Dictionary<int, int> Outcomes = new Dictionary<int, int>();
        int Counter = 0;
        int totalbonusprize = 0;
        decimal totalpaidprize = 0;
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
            int wincount = 0;


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
                if (symbol != Settings.BonusSymbol)
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
            
            decimal totalstake = 0;
            decimal rtpAgain = 0;
            int bonusgames = 1;
            
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
                    
                    bonusgames++;
                    for (int l = 0; l < Settings.Reels.Length; l++)
                    {
                        int stop = Rnd.Next(0, Settings.Reels[l].Length - 2);
                        Settings.Reels[l] = new int[] { Settings.Reels[l][stop], Settings.Reels[l][stop + 1], Settings.Reels[l][stop + 2] };
                    }
                    BonusGame();
                   
                }
            }
                Console.WriteLine($"Ticket: {string.Join(",", TicketDetails)}");
                Console.WriteLine($"Total Prize: {totalpaidprize}");
                Console.WriteLine($"Total Stake: {totalstake}");
                Console.WriteLine($"Balance: {Request.Balance}");
                Console.WriteLine($"RTP: {rtp.ToString("F10")}");
                Console.WriteLine($"Bonus prize av: {totalbonusprize / bonusgames}");
                foreach (var val in Outcomes.Values)
                {
                    Console.WriteLine(val);
                }

        }
        public void BonusGame()
        {
            Settings.SetVersion();
            for (int i = 0; i < Settings.Reels[0].Length - 2; i++)
            {

                int[] reel1 = new int[] { Settings.Reels[0][i], Settings.Reels[0][i + 1], Settings.Reels[0][i + 2] };
                for (int j = 0; j < Settings.Reels[1].Length - 2; j++)
                {
                    int[] reel2 = new int[] { Settings.Reels[1][j], Settings.Reels[1][j + 1], Settings.Reels[1][j + 2] };
                    for (int k = 0; k < Settings.Reels[2].Length - 2; k++)
                    {
                        int[] reel3 = new int[] { Settings.Reels[2][k], Settings.Reels[2][k + 1], Settings.Reels[2][k + 2] };

                        int[][] reels = new[] { reel1, reel2, reel3 };
                        List<int> bonusprizes = new List<int>();
                        foreach (var winline in Settings.Winlines)
                        {
                            List<int> line = new List<int>();
                            for (int l = 0; l < winline.Length; l++)
                                line.Add(reels[l][winline[l]]);

                            int wildsymbolcount = 0;
                            int symbol = 0;
                            BonusPrize = 0;
                            for (int l = 0; l < line.Count; l++)
                            {
                                if (line[l] == Settings.WildSymbol)
                                    wildsymbolcount++;
                                else
                                    symbol = line[l];
                            }


                            if (wildsymbolcount == Settings.NumberToMatch)
                                BonusPrize = Settings.BonusWinMultipliers[Settings.WildSymbol - 1] * Request.Stake;
                            else
                            {
                                int count = line.Count(x => x == symbol);
                                if (count + wildsymbolcount == Settings.NumberToMatch)
                                    BonusPrize = Settings.BonusWinMultipliers[symbol - 1] * Request.Stake;
                            }
                            bonusprizes.Add(BonusPrize);
                            
                        }
                        totalbonusprize += bonusprizes.Sum();
                        Console.WriteLine(totalbonusprize);
                    }
    
                }
                
            }
           

            //Console.WriteLine($"{string.Join(",", Settings.Reels[0][0], Settings.Reels[1][0], Settings.Reels[2][0])}");
            //Console.WriteLine($"{string.Join(",", Settings.Reels[0][1], Settings.Reels[1][1], Settings.Reels[2][1])}");
            //Console.WriteLine($"{string.Join(",", Settings.Reels[0][2], Settings.Reels[1][2], Settings.Reels[2][2])}");

        }
    }
}
