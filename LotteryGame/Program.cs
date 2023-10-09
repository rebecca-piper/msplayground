using System;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace LotteryGame
{
    class Program
    {
        static void Main(string[] args)
        {         
            var SQLclass = new SQLdata();
            var lottery = new Lottery();
            var randomizer = new Game();
            var menuOption = 0;
            while (true)
            {
               
                    try
                    {
                        Console.WriteLine("New game: Press 1 to play a new game");
                        Console.WriteLine("Review games: Press 2 to review all your games");
                        Console.WriteLine("Exit: Press any number to exit");

                        menuOption = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid input. Please use numbers only");
                    }


                    if (menuOption != 1 && menuOption != 2 && menuOption != 3)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        switch (menuOption)
                        {
                            case 1:
                                try
                                {
                                    SQLclass.DBplayerInsert(SQLclass.Playerusername);
                                }
                                catch (SqlException e)
                                {
                                    Console.WriteLine(e.ToString() + "Error in inserting a player");
                                }


                                Console.WriteLine("Welcome to the lottery");
                                    Console.WriteLine("------------------------");
                                    Console.WriteLine("You will be asked to enter 6 numbers.");
                                    Console.WriteLine("You will win a prize if you match 3 or more numbers with the lottery tickets, with each prize being bigger with the more numbers matched!");
                                try
                                {
                                    try
                                    {
                                        lottery.GetUserNumbers();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error in getting player numbers");
                                    }


                                    Console.WriteLine("Numbers well received");
                                    Console.WriteLine("------------------------");
                                    Console.WriteLine("Lottery Numbers");
                                    lottery.GetRandomNumbers();

                                    lottery.Prizes(lottery.UserNums);
                                    SQLclass.DBgameinsert(lottery.UserNums, lottery.RandomNums, lottery.Prize);
                                    
                                }
                             catch (SqlException e)
                                {
                                    Console.WriteLine(e.ToString() + "Error in inserting a game");
                                }
                                break;
                            case 2:
                                Console.WriteLine("PLease enter your username");
                                SQLclass.Playerusername = Console.ReadLine();

                                try
                                {
                                    SQLclass.PreviewGames(SQLclass.Playerusername);
                                }
                                catch (SqlException e)
                                {
                                    Console.WriteLine(e.ToString(), "Error in pulling game results");
                                }
                                catch (InvalidOperationException e)
                                {
                                    Console.WriteLine("Connection error" + e);
                                }
                                break;
                            case 3:
                                SQLclass.ExistingGame();
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