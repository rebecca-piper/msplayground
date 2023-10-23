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

        private int[] userNums = new int[6];
        double pot;

        public double Pot { get => pot; set => pot = value; }
        public int[] UserNums { get => userNums; set => userNums = value; }




        public void ExistingGame(int pStake)
        {

            //PlayerClass.UserStake = pStake;
            Sqlclass.ExistingGame();

            //GetUserNumbers();
            Prizes(UserNums, Sqlclass.callsArr);
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {

            //sqlclass.DBgameinsert(userNums, RandomNums, Prize);
            //Prizes(UserNums, sqlclass.callsArr, PlayerClass.UserStake);
            //byte[] prize = Encoding.ASCII.GetBytes(Prizes(ServerSetup.client.UserNums, Game.Sqlclass.CallsArr).ToString());;
            byte[] prize = Encoding.ASCII.GetBytes(Program.Lottery.Prize.ToString());
            byte[] output = Encoding.ASCII.GetBytes("You matched" + Program.Lottery.MatchedNumbers + "numbers");


            
            foreach (Socket socket in ServerSetup.Sockets) // Repeat for each connected client (socket held in a dynamic array)
            {

                socket.Send(prize);
                ServerSetup.Socket.Send(output);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            
           
            //ServerSetup.ClientSocket.Shutdown(SocketShutdown.Both);
            //ServerSetup.ClientSocket.Close();
            GetRandomNumbers(5);
            Sqlclass.NewLotteryTimer(RandomNums);
            
            Console.WriteLine("New lottery was created at {0:HH:mm:ss.fff}:" + e.SignalTime);
        }
        public void SetTimer()
        {

            System.Timers.Timer timer = new System.Timers.Timer(30000);

            timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            timer.AutoReset = true;
            timer.Start();

        }
    }
}
