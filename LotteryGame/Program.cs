
using System.Diagnostics.Metrics;

namespace LotteryGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the lottery");
            Console.WriteLine("------------------------");
            Console.WriteLine("You will be asked to enter 6 numbers.");
            Console.WriteLine("You will win a prize if you match 3 or more numbers with the lottery tickets, with each prize being bigger with the more numbers matched!");

            int[] userNumbers = GetUserNumbers();
            Console.WriteLine("Numbers well received");
            Console.WriteLine("------------------------");
            Console.WriteLine("Lottery Numbers");
            int[] randomNumbers = GetRandomNumbers();
            int matchedNumbers = MatchingNumbers(userNumbers, randomNumbers);

            if (matchedNumbers == 6)
            {
                Console.WriteLine("You matched 6 numbers");
                Console.WriteLine("Congrats, you win £50!");
            }
            else if(matchedNumbers == 5) 
            {
                Console.WriteLine("You matched 5 numbers");
                Console.WriteLine("Congrats, you win £25!");
            }
            else if (matchedNumbers == 4)
            {
                Console.WriteLine("You matched 4 numbers");
                Console.WriteLine("Congrats, you win £10!");
            }
            else if (matchedNumbers == 3)
            {
                Console.WriteLine("You matched 3 numbers");
                Console.WriteLine("Congrats, you win £5!");
            }
            else
            {
                Console.WriteLine("You didn't match 3 or more numbers :(");
                Console.WriteLine("Better luck next time");
            }
        }

        public static int[] GetUserNumbers()
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
        //public static int[] GetRandomNumbers()
        //{

        //    int Min = 0;
        //    int Max = 20;

        //    // this declares an integer array with 6 elements
        //    // which are made of random numbers
        //    int[] randomNumbers = new int[6];

        //    Random randNum = new Random();
        //    for (int i = 0; i < randomNumbers.Length; i++)
        //    {
        //         int num = randNum.Next(Min, Max);
        //        randomNumbers[i] = Convert.ToInt32(num);
        //        //if (!randomNumbers.Contains(Convert.ToInt32(randNum)))
        //        //{
        //        //    randomNumbers[i] =  Convert.ToInt32(randNum);
        //        //}
        //        //Console.WriteLine("");
        //    }
        //    foreach (var number in randomNumbers)
        //    {
        //        Console.WriteLine(number.ToString());
        //    }
        //    return randomNumbers;
        //}

        public static int[] GetRandomNumbers()
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

        public static int MatchingNumbers(int[] usernumbers, int[] randomNumbers)
        {
            return usernumbers.Intersect(randomNumbers).Count();
        }
    }
}