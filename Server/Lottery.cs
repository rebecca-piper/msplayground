using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Server
{

    public class Lottery : Game
    {  
        double currentPot;
        private static List<Clients> clients = new List<Clients>();
        
        public double CurrentPot { get => currentPot; set => currentPot = value; }
        internal static List<Clients> Clients { get => clients; set => clients = value; }

        public void PlayExistingGame()
        {            
            Sqlclass.GetExistingGame();
            currentPot = Game.Sqlclass.StoredPot + ServerSetup.client.Userstake;
            GetPrizes(ServerSetup.client.UserNums, Sqlclass.callsArr);
            ServerSetup.client.Prize = Prize;
            Clients.Add(ServerSetup.client);
            //Sqlclass.DBgameinsert(ServerSetup.client.UserNums, Sqlclass.callsArr);
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {

            //sqlclass.DBgameinsert(userNums, RandomNums, Prize);

            byte[] output = Encoding.ASCII.GetBytes("You matched" + Program.Lottery.MatchedNumbers + "numbers");
            byte[] prize = null;
            List<byte[]> prizes = null;
            prizes = new List<byte[]>();
            foreach (Clients client in clients)
            {
                prize = Encoding.ASCII.GetBytes(client.Prize.ToString());
                prizes.Add(prize);             
            }

            for (int i = 0; i < ServerSetup.Sockets.Count; i++)
            {
                ServerSetup.Sockets[0].Send(prizes[0]);
            }
            //socket.Send(output);
            //socket.Shutdown(SocketShutdown.Both);
            //socket.Close();


            ServerSetup.Sockets.Clear();
            GetRandomNumbers(5);
            Sqlclass.NewLotteryTimer(RandomNums);
            
            Console.WriteLine("New lottery was created at {0:HH:mm:ss.fff}:" + e.SignalTime);
        }
        public void SetTimer()
        {

            System.Timers.Timer timer = new System.Timers.Timer(60000);

            timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            timer.AutoReset = true;
            timer.Start();

        }
    }
}
