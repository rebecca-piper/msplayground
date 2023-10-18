using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace LotteryGame
{
    public class Threads
    {
        public readonly List<Player> PlayerList = new List<Player>();
        public readonly List<Lottery> LotteryList = new List<Lottery>();

        // Make some example games and players for testing
        public void CreateObject()
        {
            for (int i = 0; i < 3; i++)
            {
                LotteryList.Add(new Lottery() {PlayerClass = new Player() { Playerusername=$"player{i+1}", UserStake=5 } });
            }

            // print the list of players
            Console.WriteLine("\nCurrent players ready to play:");
            foreach (Lottery l in LotteryList)
            {
                Console.WriteLine(l.PlayerClass);
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
            for (int i = 0; i < LotteryList.Count; i++)
            {
                Lottery l = LotteryList[i];
                Thread thread = new Thread(l.PlayGame);
                // give the thread a recognisable name
                thread.Name = $"objthread{++threadNum}";
                thread.Start();
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
