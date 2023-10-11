using System;
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
 
        public int[] UserNums { get => userNums; set => userNums = value; }
        
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
                                throw new Exception();
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
            sqlclass.ExistingGame();
            GetUserNumbers();
            Prizes(UserNums, sqlclass.callsArr, UserStake);
        }


    }
}
