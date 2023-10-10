using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame
{
    public class Game
    {
            
            int[] randomNums = new int[6];
            int[] userrandomNums = new int[6];
            int prize;
           
            public int[] RandomNums { get => randomNums; set => randomNums = value; }
            public int Prize { get => prize; set => prize = value; }
        public int[] UserrandomNums { get => userrandomNums; set => userrandomNums = value; }

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
            for (int i = 0; i < range.Length; i++)
            {

                int numIndex = rnd.Next(range.Length);


                int temp = range[i];
                range[i] = range[numIndex];
                range[numIndex] = temp;
            }
            Array.Copy(range, userrandomNums, userrandomNums.Length);
            Console.WriteLine(String.Join(", ", userrandomNums));
            return randomNums;
            }
        public void AutoPlay()
        {
            for (int i = 0; i < 10; i++)
            {
                GetRandomNumbers();
                Prizes(UserrandomNums);
            }
        }
        public void Prizes(int[] pUsernums)
        {

            Console.WriteLine("-----------");
            Console.WriteLine("Winning numbers");
            var winningnums = userrandomNums.Intersect(randomNums);
            int matchedNumbers = winningnums.Count();

            foreach (var number in winningnums)
            {
                Console.WriteLine(number.ToString());
            }
            switch (matchedNumbers)
            {
                case 3:
                    prize = 5;
                    Console.WriteLine("You matched 3 numbers");
                    Console.WriteLine("Congrats, you win £" + prize);
                    break;
                case 4:
                    prize = 10;
                    Console.WriteLine("You matched 4 numbers");
                    Console.WriteLine("Congrats, you win £" + prize);
                    break;
                case 5:
                    prize = 25;
                    Console.WriteLine("You matched 5 numbers");
                    Console.WriteLine("Congrats, you win £" + prize);
                    break;
                case 6:
                    prize = 50;
                    Console.WriteLine("You matched 6 numbers");
                    Console.WriteLine("Congrats, you win £" + prize);
                    break;
                default:
                    Console.WriteLine("You didn't match 3 or more numbers :(");
                    Console.WriteLine("Better luck next time");
                    break;

            }

        }

    }
}

