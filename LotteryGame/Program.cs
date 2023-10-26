using System;
using System.Data.Common;
using System.Numerics;
using Microsoft.Data.SqlClient;
using System.Threading;
using System.Text;
using System.Reflection;
using System.Net.Sockets;
using System.Net;

namespace LotteryGame
{
    class Program
    {    
        private static Player player = new Player();
        private static SQLClient sqlclass = new SQLClient();
        public static Player Player { get => player; set => player = value; }
        //public static Threads Tclass { get => tclass; set => tclass = value; }
        public static Client client = new Client();      
        static void Main(string[] args)
        {
            var menuOption = 0;
            while (true)
            {
                    try
                    {
                    //Console.WriteLine("New game: Press 1 to play a new game");
                    //Console.WriteLine("Preview games: Press 2 to preview all your games");
                    //Console.WriteLine("Existing games: Press 3 to play existing game");                  
                        Console.WriteLine("Press 1 to join a new lottery");
                        Console.WriteLine("Press 2 to reveal the latest lottery you played");
                        Console.WriteLine("Exit: Press any number to exit");
                    menuOption = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid input. Please use numbers only");
                    }

                    if (menuOption > 2)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                    switch (menuOption)
                    {
                        case 1:
                            //        SQLclass.DBplayerInsert();
                            //        Console.WriteLine("Welcome to the lottery");
                            //        Console.WriteLine("------------------------");
                            //        Console.WriteLine("You will be asked to enter 6 numbers.");
                            //        Console.WriteLine("You will win a prize if you match 3 or more numbers with the lottery tickets, with each prize being bigger with the more numbers matched!");
                            //        lotteryclass.GetUserNumbers();
                            //        Console.WriteLine("Numbers well received");
                            //        Console.WriteLine("------------------------");
                            //        Console.WriteLine("Lottery Numbers");
                            //        lotteryclass.GetRandomNumbers(menuOption);

                            //lotteryclass.Prizes(lotteryclass.UserNums, SQLclass.CallsArr, player.UserStake);
                            //        SQLclass.NewLotteryInsert(lotteryclass.UserNums, lotteryclass.RandomNums, lotteryclass.Prize, lotteryclass.Pot);
                            //        break;
                            player.GetPlayerRequest();
                            break;
                        case 2:
                            player.GetPlayerName();
                            sqlclass.PreviewGames();
                            client.GetPlayerGame();
                            break;
                        case 3:
                            //tclass.CreateObject();
                            //tclass.CreateThreads();
                            ////SQLclass.DBplayerInsert();
                            ////player.Stake();
                            ////lotteryclass.ExistingGame(player.UserStake);
                            ////SQLclass.DBgameinsert(lotteryclass.UserNums, lotteryclass.RandomNums, lotteryclass.Prize);
                            //lotteryclass.PlayGame();
                            break;
                        case 4:
                            //lotteryclass.AutoPlay();
                            break;
                        default:
                            Console.WriteLine("Press any key to exit");
                            Console.ReadKey();
                            Environment.Exit(0);
                            break;
                        }
                    }
            }
        }
       
    }
}