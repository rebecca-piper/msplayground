using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame
{
    public class Game
    {
        public SQLdata sqlclass = new SQLdata();
     
        int[] randomNums = new int[6];
            int[] userrandomNums = new int[6];
            double prize;
        int[] stakeArr = new int[5];
        double[] multiplier = new double[4];
        int userStake;
            public int[] RandomNums { get => randomNums; set => randomNums = value; }
            public double Prize { get => prize; set => prize = value; }
        public int[] UserrandomNums { get => userrandomNums; set => userrandomNums = value; }
        public int UserStake { get => userStake; set => userStake = value; }
      

        public int[] GetRandomNumbers(int menuOption)
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
            if (menuOption == 1)
            {
                Console.WriteLine(String.Join(", ", randomNums));
            }
                
            for (int i = 0; i < range.Length; i++)
            {

                int numIndex = rnd.Next(range.Length);


                int temp = range[i];
                range[i] = range[numIndex];
                range[numIndex] = temp;
            }
            Array.Copy(range, userrandomNums, userrandomNums.Length);
            if (menuOption == 4)
            {
                Console.WriteLine(String.Join(", ", userrandomNums));
            }
            
            return randomNums;
            }
        public void AutoPlay()
        {

            for (int i = 0; i < 10; i++)
            {
                GetRandomNumbers(4);
                Prizes(UserrandomNums, sqlclass.callsArr, userStake);
            }
        }
        public void Stake()
        {

            int[] stakeArr = { 5, 10, 25, 50 };
            Console.WriteLine("Enter your stake from" + String.Join(", ", stakeArr));
            userStake = Convert.ToInt32(Console.ReadLine());
            
            
        }
        public void Prizes(int[] pUsernums, int[] calls, int pStake)
        {
            userStake = pStake;
            int matchedNumbers = 0;
           
            double[] multiplier = { 1, 1.25, 1.5, 2};
            Console.WriteLine("-----------");
            Console.WriteLine("Winning numbers");
           if (pUsernums != null && calls[0] != 0)
            {
                var winningnums = pUsernums.Intersect(calls);
                matchedNumbers = winningnums.Count();
                foreach (var number in winningnums)
                {
                    Console.WriteLine(number.ToString());
                }
            }
          else if (pUsernums != null)
            {
              var winningnums = pUsernums.Intersect(randomNums);
                matchedNumbers = winningnums.Count();
                foreach (var number in winningnums)
                {
                    Console.WriteLine(number.ToString());
                }
            }

            else if (userrandomNums != null)
            {
                var winningnums = userrandomNums.Intersect(randomNums);
                matchedNumbers = winningnums.Count();
                foreach (var number in winningnums)
                {
                    Console.WriteLine(number.ToString());
                }
            }
           
            switch (matchedNumbers)
            {
                case 3:
                    prize = multiplier[0] * pStake;
                 
                    break;
                case 4:
                    prize = multiplier[1] * pStake;

                    break;
                case 5:
                    prize = multiplier[2] * pStake;

                    break;
                case 6:
                    prize = multiplier[3] * pStake;

                    break;
                default:
                    Console.WriteLine("You didn't match 3 or more numbers :(");
                    Console.WriteLine("Better luck next time");
                    break;
                    
            }
            if (matchedNumbers >= 3)
            {
                Console.WriteLine("You matched" + matchedNumbers +  "numbers");
                Console.WriteLine("Congrats, you win £" + prize);
            }

        }

    }
}

