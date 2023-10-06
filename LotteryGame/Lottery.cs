using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame
{
    public class Lottery : Game
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

       
        public void ExistingGame()
        {

        }
       
         
    }
}
