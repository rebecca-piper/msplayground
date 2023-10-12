﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame
{

    public class Lottery : Game
    {
        
        int[] userNums = new int[6];
        double pot;
        public int[] UserNums { get => userNums; set => userNums = value; }
        public double Pot { get => pot; set => pot = value; }

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
                    try
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
                                Console.WriteLine("Number out of range. Please enter a number betweeon 0 and 20");
                            }
                            else if (userNums.Contains(userNum))
                            {
                                Console.WriteLine("Number already entered. Repeated numbers are not allowed. Please try again");
                            }

                        }
                    }
                    catch (Exception e)
                    { 
                        Console.WriteLine("Invalid input. Please enter a number betweeon 0 and 20"); 
                    }    
                }

            }
            return userNums;
        }


        public void ExistingGame()
        {
            Player playerstake = new Player();
            
            sqlclass.ExistingGame();
            pot = playerstake.UserStake + sqlclass.StoredPot;
            GetUserNumbers();
            Prizes(UserNums, sqlclass.callsArr, playerstake.UserStake);
        }


    }
}
