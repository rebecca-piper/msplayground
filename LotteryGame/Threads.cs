using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LotteryGame
{
    public class Threads
    {
        public readonly List<Player> PlayerList = new List<Player>();
        public void CreateObject()
        {
            for (int i = 0; i < 3; i++)
            {
                PlayerList.Add(new Player() { Playerusername=$"player{i+1}", Pot=(double)50, StakeArr=new int[]{5}, UserStake=5});
            }

            // print the list of players
            Console.WriteLine("Current players ready to play:");
            foreach (Player p in PlayerList)
            {
                Console.WriteLine(p);
            } Console.WriteLine();
        }

        public void CreateThreads()
        {

            //for (int i =0; i < PlayerList.Count; i++)
            //  {
            //      //ThreadStart obj = new ThreadStart(CreateObject);
            //      //Thread thread = new Thread(obj);
            //      Thread thread = new Thread(CreateObject)
            //      {
            //          Name = "obj thread"

            //      };
            //      thread.Start();
            //  }
            //ThreadStart obj = new ThreadStart(CreateObject);
            //Thread thread = new Thread(obj);
            int threadNum = 0;
            foreach (var thisPlayer in PlayerList.ToList())
            {
                var thread = new Thread(
                       () => Program.Lotteryclass.PlayGame(thisPlayer));
                thread.Name = $"obj thread {++threadNum}";
                thread.Start();

//                Thread thread = new Thread(new ThreadStart(Program.Lotteryclass.PlayGame(thisPlayer)))
//                {
//                    // give the thread a recognisable name
//                    Name = $"obj thread {++threadNum}"
//                };
//                thread.Start();
            }

            //Thread t1 = new Thread(Program.SQLclass.DBplayerInsert);
            //t1.Start();

            //Thread t2 = new Thread(Program.SQLclass.DBplayerInsert);
            //t2.Start();

            //Thread t3 = new Thread(Program.SQLclass.DBplayerInsert);
            //t3.Start();




        }
    }
}
