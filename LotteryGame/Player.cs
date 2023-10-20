using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame
{
    public class Player
    {
        string playerusername;
       
        int userStake;
        int[] stakeArr = new int[5];
        private int[] userNums = new int[6];
        public string Playerusername { get => playerusername; set => playerusername = value; }
     
        public int UserStake { get => userStake; set => userStake = value; }
        public int[] StakeArr { get => stakeArr; set => stakeArr = value; }
        public int[] UserNums { get => userNums; set => userNums = value; }

        public void GetPlayerName()
        {

            bool isValidInput = false;

            while (!isValidInput)
            {
                try
                {
                    Console.WriteLine("Please enter a username");
                    playerusername = Console.ReadLine();
                    isValidInput = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in getting player username");
                }
            }
        }
        public void GetStake()
        {

            int[] stakeArr = { 5, 10, 25, 50 };
            while (true)
            {
                Console.WriteLine("Enter your stake from: " + String.Join(", ", stakeArr));
                userStake = Convert.ToInt32(Console.ReadLine());
                if (!stakeArr.Contains(userStake))
                {
                    Console.WriteLine("Invalid stake. Please choose a stake from the below:");
                    continue;
                }
                else
                {
                    //pot = SQLclass.StoredPot + userStake;
                    break;
                }
            }

        }
        public void  GetUserNumbers()
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
        }
        public void GetPlayerRequest()
        {
            GetPlayerName();
            GetStake();
            GetUserNumbers();
        }
    }
}
