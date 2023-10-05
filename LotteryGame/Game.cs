using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame
{
    public class Game
    {
        int[] userNums = new int[6];
        int[] randomNums = new int[6];
        int prize;
        public int[] UserNums { get => userNums; set => userNums = value; }
        public int[] RandomNums { get => randomNums; set => randomNums = value; }
        public int Prize { get => prize; set => prize = value; }

        public  int[] GetUserNumbers()
        {
            userNums = new int[6];
            Console.WriteLine("Please enter six numbers from 0-20");
           

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
            int min = 1;
            int max = 20;
            int[] range = new int[max - min + 1];
            for (int i = 0; i < range.Length; i++)
            {
                range[i] = min++;
            }
            Random rnd = new Random();

            for (int i = 0; i < range.Length; i++)
            {
                
                int numIndex = rnd.Next(range.Length);

                
                int temp = range[i];
                range[i] = range[numIndex];
                range[numIndex] = temp;
            }
            
            Array.Copy(range, randomNums, randomNums.Length);
            Console.WriteLine(String.Join(", ", randomNums));
            return randomNums;
        }
        public void ExistingGame()
        {

        }
        public void Prizes()
        {
                     
            Console.WriteLine("-----------");
            Console.WriteLine("Winning numbers");
            var winningnums = userNums.Intersect(randomNums);
            int matchedNumbers = winningnums.Count();
            
            foreach (var number in winningnums)
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
