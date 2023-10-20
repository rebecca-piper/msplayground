using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
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

            PlayerClass.UserStake = pStake;
            sqlclass.ExistingGame(pStake);

            GetUserNumbers();
            Prizes(UserNums, sqlclass.callsArr, pStake);
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {

            sqlclass.DBgameinsert(userNums, RandomNums, Prize);
            Prizes(UserNums, sqlclass.callsArr, PlayerClass.UserStake);
            GetRandomNumbers(5);
            sqlclass.NewLotteryTimer(RandomNums);

            Console.WriteLine("New lottery was created at {0:HH:mm:ss.fff}:" + e.SignalTime);
        }
        public void SetTimer()
        {

            System.Timers.Timer timer = new System.Timers.Timer(3000);

            timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            timer.AutoReset = false;
            timer.Start();

        }
    }
}
