using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LotteryGame
{
    public class Game
    {
        public SQLdata sqlclass = new SQLdata();
        private Player playerClass = new Player();
        int[] randomNums = new int[6];
            int[] userrandomNums = new int[6];
        
        double prize;
        int[] stakeArr = new int[5];
        double[] multiplier = new double[7];
        int userStake;
        int matchedNumbers;
        public int[] RandomNums { get => randomNums; set => randomNums = value; }
            public double Prize { get => prize; set => prize = value; }
        public int[] UserrandomNums { get => userrandomNums; set => userrandomNums = value; }
        public int UserStake { get => userStake; set => userStake = value; }
        public Player PlayerClass { get => playerClass; set => playerClass = value; }
        public int MatchedNumbers { get => matchedNumbers; set => matchedNumbers = value; }

        public Game()
        {
            Console.WriteLine($"Game created, hash {this.GetHashCode()}");
        }
  

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
                Console.WriteLine(String.Join(", ", randomNums));
                Console.WriteLine(String.Join(", ", userrandomNums));
            }
            
            return randomNums;
            }
        public void AutoPlay()
        {

            for (int i = 0; i < 10; i++)
            {
                GetRandomNumbers(4);
                Prizes(UserrandomNums, sqlclass.callsArr, playerClass.UserStake);
            }
        }
     
        public void Prizes(int[] pUsernums, int[] calls, int pStake )
        {
            playerClass.UserStake = pStake;
            
           
            double[] multiplier = { 0, 0, 0 , 1, 1.25, 1.5, 2};
            Console.WriteLine("-----------");
            Console.WriteLine("Winning numbers");
            int[] winningnums = new int[6];
           if (pUsernums != null && calls[0] != 0)
            {
                winningnums = pUsernums.Intersect(calls).ToArray();
                
            }
          else if (pUsernums != null)
            {
               winningnums = pUsernums.Intersect(randomNums).ToArray();
                
            }

       
            matchedNumbers = winningnums.Count();
            foreach (var number in winningnums)
            {
                Console.WriteLine(number.ToString());
            }

            if (matchedNumbers >= 3 && matchedNumbers < 6)
            {
                prize = multiplier[matchedNumbers] * pStake;
                Console.WriteLine("You matched" + matchedNumbers +  "numbers");
                Console.WriteLine("Congrats, you win £" + prize);
            }
            else if (matchedNumbers == 6)
            {
                Console.WriteLine("You matched" + matchedNumbers + "numbers");
                Console.WriteLine("Congrats, you won the jackpot £" + sqlclass.StoredPot);
            }
            else
            {
                Console.WriteLine("You didn't match 3 or more numbers :(");
                Console.WriteLine("Better luck next time");
            }
        }

      public void PlayGame(object thisPlayer)
      {
            PlayerClass = (Player)thisPlayer;

            Thread thread = Thread.CurrentThread;
            Console.WriteLine($"[PlayGame] thread.name:{thread.Name} player:{{{PlayerClass}}} gamehash:{this.GetHashCode()}");

            sqlclass.DBplayerInsert(PlayerClass);
            playerClass.Stake();
            Program.Lotteryclass.ExistingGame(playerClass.UserStake);

            sqlclass.DBgameinsert(PlayerClass, Program.Lotteryclass.UserNums, Program.Lotteryclass.RandomNums, Program.Lotteryclass.Prize);
      }
    }
}

