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
            var game = new Game();
            int prizes = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine("New game: Press 1 to play a new game");
                    Console.WriteLine("Review games: Press 2 to review all your games");
                    Console.WriteLine("Exit: Press any number to exit");

                    var menuOption = Convert.ToInt32(Console.ReadLine());
                    if (menuOption != 1 && menuOption != 2)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        switch (menuOption)
                        {
                            case 1:
                                

                                SQLclass.DBplayerInsert(SQLclass.Playerusername);

                                Console.WriteLine("Welcome to the lottery");
                                Console.WriteLine("------------------------");
                                Console.WriteLine("You will be asked to enter 6 numbers.");
                                Console.WriteLine("You will win a prize if you match 3 or more numbers with the lottery tickets, with each prize being bigger with the more numbers matched!");

                                int[] userNumbers = game.GetUserNumbers();
                                Console.WriteLine("Numbers well received");
                                Console.WriteLine("------------------------");
                                Console.WriteLine("Lottery Numbers");
                                int[] randomNumbers = game.GetRandomNumbers();
                                int matchedNumbers = userNumbers.Intersect(randomNumbers).Count();
                                game.Prizes(matchedNumbers, userNumbers, randomNumbers);
                                SQLclass.DBgameinsert(userNumbers, randomNumbers, SQLclass.Playerusername, SQLclass.Prizes);
                                break;
                            case 2:
                                Console.WriteLine("PLease enter your username");
                                SQLclass.Playerusername = Console.ReadLine();

                                SQLclass.PreviewGames(SQLclass.Playerusername);
                                break;
                            case 3:
                                break;
                            default:
                                Console.WriteLine("Press any key to exit");
                                Console.ReadKey();
                                Environment.Exit(0);
                                break;
                        }
                    }

                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("Connection error" + e);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid input. Please use numbers only");
                }

            }
        }
       
    }
}