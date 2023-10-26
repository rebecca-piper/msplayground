﻿using System;
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
        public double CurrentPot { get => currentPot; set => currentPot = value; }
        public void PlayExistingGame()
        {            
            Sqlclass.GetExistingGame();
            currentPot = Game.Sqlclass.StoredPot + ServerSetup.client.Userstake;
            GetPrizes(ServerSetup.client.UserNums, Sqlclass.callsArr);
            ServerSetup.client.Prize = Prize;
            Sqlclass.DBgameinsert(ServerSetup.client.UserNums, Sqlclass.callsArr);
            Sqlclass.UpdatePot();
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            byte[] prize = null;
            Sqlclass.GetExistingGame();
            //prize = Encoding.ASCII.GetBytes(ServerSetup.client.Prize.ToString());
            foreach (Socket socket in ServerSetup.Clients.Keys)
            {
                if (ServerSetup.Clients[socket].Prize == 0)
                {
                    byte[] output = Encoding.ASCII.GetBytes("You didn't win anything");
                    socket.Send(output);
                }
                else if (Program.Lottery.MatchedNumbers == 6)
                {
                    ServerSetup.Clients[socket].Prize = Sqlclass.StoredPot;
                    prize = Encoding.ASCII.GetBytes("Congrats you just won the jackpot: £" + ServerSetup.Clients[socket].Prize.ToString());
                    socket.Send(prize);
                }
                else
                {
                    prize = Encoding.ASCII.GetBytes("Congrats you won £" + ServerSetup.Clients[socket].Prize.ToString());
                    socket.Send(prize);
                }
               
              
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            //List<byte[]> prizes = null;
            //prizes = new List<byte[]>();
            //foreach (Clients client in clients)
            //{
            //    prize = Encoding.ASCII.GetBytes(client.Prize.ToString());
            //    prizes.Add(prize);
            //}

            //for (int i = 0; i < ServerSetup.Sockets.Count; i++)
            //{
            //    ServerSetup.Sockets[i].Send(prizes[i]);
            //}

            //socket.Shutdown(SocketShutdown.Both);
            //socket.Close();
            ServerSetup.Clients.Clear();
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
