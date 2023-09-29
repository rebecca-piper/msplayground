
using System.Diagnostics.Metrics;

namespace LotteryGame
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] userNumbers = GetUserNumbers();
            int[] randomNumbers = GetRandomNumbers();
            
        }

        public static int[] GetUserNumbers()
        {
            int[] userNums = new int[6];

            for (int i = 0; i < 6; i++)
            {
                int userNum;
               
                Console.WriteLine("Value:");

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
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a number betweeon 0 and 20");
                        }
                    }
                    
                }

            }
            return userNums;
        }
        public static int[] GetRandomNumbers()
        {

            int Min = 0;
            int Max = 20;

            // this declares an integer array with 6 elements
            // which are made of random numbers
            int[] randomNumbers = new int[6];

            Random randNum = new Random();
            for (int i = 0; i < randomNumbers.Length; i++)
            {
                randomNumbers[i] = randNum.Next(Min, Max);
            }
            foreach (var number in randomNumbers)
            {
                Console.WriteLine(number.ToString());
            }
            return randomNumbers;
        }
    }
}