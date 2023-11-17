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
        public float Probability;
        public int[] GetTicket()
        {
            Settings.SetVersion();
            TicketDetails = new int[Settings.TicketSize];

            int[] winsymbol = new int[3];
            int winsymbol2 = 0;
            int winsymbol3 = 0;
            //for (int i = 0; i < 3; i++)
            //{

            int[] winchance = new int[3];

            for (int i = 0; i < winchance.Length; i++)
            {
                winchance[i] = rnd.Next(1, 10000000);
                if (winchance[i] < Settings.Outcomes[6])
                {
                    winsymbol[i] = 7;

                }
                else if (winchance[i] < Settings.Outcomes[5])
                {
                    winsymbol[i] = 6;

                }
                else if (winchance[i] < Settings.Outcomes[4])
                {
                    winsymbol[i] = 5;

                }
                else if (winchance[i] < Settings.Outcomes[3])
                {
                    winsymbol[i] = 4;

                }
                else if (winchance[i] < Settings.Outcomes[2])
                {
                    winsymbol[i] = 3;

                }
                else if (winchance[i] < Settings.Outcomes[1])
                {
                    winsymbol[i] = 2;

                }
                else if (winchance[i] < Settings.Outcomes[0])
                {
                    winsymbol[i] = 1;

                }

            }
            Outcome(winsymbol);
            return TicketDetails;
        }

        public void Outcome(int[] winsymbol)
        {
            if (winsymbol[0] != 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    TicketDetails[i] = winsymbol[0];
                }


            }
            if (winsymbol[1] != 0)
            {
                for (int i = 3; i < 6; i++)
                {
                    TicketDetails[i] = winsymbol[1];
                }



            }
            if (winsymbol[2] != 0)
            {
                for (int i = 6; i < 9; i++)
                {
                    TicketDetails[i] = winsymbol[2];
                }


            }
            for (int j = 0; j < TicketDetails.Length; j++)
            {
                if (TicketDetails[j] == 0)
                {
                    do
                    {
                        TicketDetails[j] = rnd.Next(1, 8);


                    }
                    while (TicketDetails.Count(s => s == TicketDetails[j]) > 2);
                }

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
            float rtp = 0;
            float totalpaidprize = 0;
            float totalstake = 0;

            for (int i = 0; i < 10000; i++)
            {
                List<int> prizes = new List<int>(); ;

                Request.GetRequest();
                GetTicket();
                Console.WriteLine(string.Join(",", TicketDetails));
                for (int matchedSymbol = 1; matchedSymbol <= Settings.NumberOfSymbols; matchedSymbol++)
                {
                    Prize = 0;
                    int count = TicketDetails.Count(x => x == matchedSymbol);
                    if (count >= Settings.NumberToMatch)
                    {
                        Prize = Settings.WinMultipliers[matchedSymbol - 1] * Request.Stake;
                        Request.Balance = Request.Balance + Prize;
                    }
                    prizes.Add(Prize);
                }


                Console.WriteLine($"Prize: {prizes.Sum()}");
                totalpaidprize = totalpaidprize + prizes.Sum();
                totalstake = totalstake + Request.Stake;
                rtp = totalpaidprize / totalstake * 100;


                //rtp = multiplier * (Probability) * 100;
                //rtpperc = rtpperc + rtp;

                Console.WriteLine($"Balance: {Request.Balance}");
                Console.WriteLine($"RTP: {rtp.ToString("F")}");
            }
            bool bonus = TicketDetails.Contains(Settings.BonusSymbol);
            if (bonus)
            {
                int SpinOutcome = rnd.Next(1, Settings.BonusSymbolSize);
                BonusPrize = Settings.BonusWinMultipliers[SpinOutcome - 1] * Request.Stake;
                Console.WriteLine($"Bonus Prize: {Prize}");
            }
        }
    }

}
