using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame
{
    public class Game
    {
        int[] userNums;

        public int[] UserNums { get => userNums; set => userNums = value; }

        public  int[] GetUserNumbers()
        {
            
            Console.WriteLine("Please enter six numbers from 0-20");
            int[] userNums = new int[6];

            for (int i = 0; i < 6; i++)
            {
                int userNum;

                Console.WriteLine("Number:" + (i + 1));

                bool isValidInput = false;
                while (!isValidInput)
                {
                    string userValue = Console.ReadLine();

                    if (int.TryParse(userValue, out userNum))
                    {
                        if (userNum >= 0 && userNum <= 20 && !userNums.Contains(userNum))
                        {
                            userNums[i] = userNum;
                            break;
                        }
                        if (userNum <= 0 || userNum >= 20)
                        {
                            Console.WriteLine("Number is out of range. Please enter a number between 0 and 20");
                        }
                        else if (userNums.Contains(userNum))
                        {
                            Console.WriteLine("Number already entered. Repeated numbers are not allowed. Please try again");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number betweeon 0 and 20");
                    }

                }

            }
            return userNums;
        }
        public int[] GetRandomNumbers()
        {

            int Min = 1;
            int Max = 20;
            int[] randomNumbers = new int[6];

            Random randNum = new Random();
            for (int i = 0; i < randomNumbers.Length; i++)
            {
                int num = randNum.Next(Min, Max);
                bool randomNumber = false;
                while (!randomNumber)
                {
                    if (!randomNumbers.Contains(Convert.ToInt32(num)))
                    {
                        randomNumbers[i] = Convert.ToInt32(num);
                        randomNumber = true;
                    }
                    else
                    {
                        num = randNum.Next(Min, Max);
                    }
                }

            }
            foreach (var number in randomNumbers)
            {
                Console.WriteLine(number.ToString());
            }
            return randomNumbers;
        }
        public void Prizes(int matchedNumbers, int[] usernumbers, int[] randomNumbers)
        {
            Console.WriteLine("-----------");
            Console.WriteLine("Winning numbers");
            var matchednumbers = usernumbers.Intersect(randomNumbers);
            int prize;
            foreach (var number in matchednumbers)
            {
                Console.WriteLine(number.ToString());
            }

            if (matchedNumbers == 6)
            {
                prize = 50;
                Console.WriteLine("You matched 6 numbers");
                Console.WriteLine("Congrats, you win £50!");
            }
            else if (matchedNumbers == 5)
            {
                prize = 25;
                Console.WriteLine("You matched 5 numbers");
                Console.WriteLine("Congrats, you win £25!");
            }
            else if (matchedNumbers == 4)
            {
                prize = 10;
                Console.WriteLine("You matched 4 numbers");
                Console.WriteLine("Congrats, you win £10!");
            }
            else if (matchedNumbers == 3)
            {

                prize = 5;
                Console.WriteLine("You matched 3 numbers");
                Console.WriteLine("Congrats, you win £5!");
            }
            else
            {
                Console.WriteLine("You didn't match 3 or more numbers :(");
                Console.WriteLine("Better luck next time");
            }
        }
         
    }
}
