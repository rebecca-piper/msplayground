using System;
using System.Data.Common;
using Microsoft.Data.SqlClient;


namespace LotteryGame
{
    class Program
    {
        private static Lottery lotteryclass = new Lottery();

        public static Lottery Lotteryclass { get => lotteryclass; set => lotteryclass = value; }

        static void Main(string[] args)
        {
             Player player = new Player();
            SQLdata SQLclass = new SQLdata();
            


            var menuOption = 0;
            while (true)
            {
               
                    try
                    {
                        Console.WriteLine("New game: Press 1 to play a new game");
                        Console.WriteLine("Preview games: Press 2 to preview all your games");
                        Console.WriteLine("Existing games: Press 3 to play existing game");
                        Console.WriteLine("Exit: Press any number to exit");

                        menuOption = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid input. Please use numbers only");
                    }


                    if (menuOption != 1 && menuOption != 2 && menuOption != 3 && menuOption != 4)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        switch (menuOption)
                        {
                            case 1:
                                    SQLclass.DBplayerInsert();
                                    Console.WriteLine("Welcome to the lottery");
                                    Console.WriteLine("------------------------");
                                    Console.WriteLine("You will be asked to enter 6 numbers.");
                                    Console.WriteLine("You will win a prize if you match 3 or more numbers with the lottery tickets, with each prize being bigger with the more numbers matched!");
                                    lotteryclass.GetUserNumbers();
                                    Console.WriteLine("Numbers well received");
                                    Console.WriteLine("------------------------");
                                    Console.WriteLine("Lottery Numbers");
                                    lotteryclass.GetRandomNumbers(menuOption);

                                    lotteryclass.Prizes(lotteryclass.UserNums, SQLclass.CallsArr, player.UserStake);
                                    SQLclass.NewLotteryInsert(lotteryclass.UserNums, lotteryclass.RandomNums, lotteryclass.Prize, lotteryclass.Pot);
                                    break;
                            case 2:    
                                player.PlayerName();
                                SQLclass.PreviewGames();
                                break;
                            case 3:
                            SQLclass.DBplayerInsert();
                            player.Stake();
                            lotteryclass.ExistingGame(player.UserStake);
                            
                            SQLclass.DBgameinsert(lotteryclass.UserNums, lotteryclass.RandomNums, lotteryclass.Prize);
                                break;
                             case 4:

                            lotteryclass.AutoPlay();

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