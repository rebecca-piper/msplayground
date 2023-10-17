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
                PlayerList.Add(new Player());
            }
           
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
            foreach (var player in PlayerList.ToList())
            {
                lock (player)
                {
                    Thread thread = new Thread(new ThreadStart(Program.Lotteryclass.PlayGame))
                    {
                        Name = "obj thread"

                    };
                    thread.Start();
                }
               

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
