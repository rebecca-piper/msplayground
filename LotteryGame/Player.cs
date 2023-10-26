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
        
        private int[] userNums = new int[6];
        private static SQLdata SQLclass = new SQLdata();
        public string Playerusername { get => playerusername; set => playerusername = value; }
   
        public int UserStake { get => userStake; set => userStake = value; }
      
        public int[] UserNums { get => userNums; set => userNums = value; }
        public static SQLdata SQLclass1 { get => SQLclass; set => SQLclass = value; }

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
                try
                {
                    Console.WriteLine("Enter your stake from: " + String.Join(", ", stakeArr));
                    string uservalue = Console.ReadLine();
                    if (int.TryParse(uservalue, out userStake))
                    {
                        if (!stakeArr.Contains(userStake))
                        {
                            Console.WriteLine("Invalid stake. Please choose a stake from the below:");
                            continue;
                        }
                        else
                        {

                            break;
                        }
                    }
                    if (!stakeArr.Contains(Convert.ToInt32(uservalue)))
                    {
                        Console.WriteLine("Invalid input. Please enter a number betweeon 0 and 20");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input. Please enter a valid stake");
                }
            }

        }
        public void  GetUserNumbers()
        {
            userNums = new int[6];
            Console.WriteLine("Please enter six numbers from 0-20");
            int userNum;
            int min = 1;
            int max = 20;
            int[] range = new int[max - min + 1];
            for (int i = 0; i < range.Length; i++)
            {
                range[i] = min++;
            }
            for (int i = 0; i < 6; i++)
            {
             
                Console.WriteLine("Number:" + (i + 1));

                bool isValidInput = false;
                while (!isValidInput)
                {
                    try
                    {
                        string userValue = Console.ReadLine();

                        if (int.TryParse(userValue, out userNum))
                        {
                            if (range.Contains(userNum) && !userNums.Contains(userNum))
                            {
                                userNums[i] = userNum;
                                break;
                            }
                            if (!range.Contains(userNum))
                            {
                                Console.WriteLine("Number out of range. Please enter a number betweeon 0 and 20");
                            }
                            else if (userNums.Contains(userNum))
                            {
                                Console.WriteLine("Number already entered. Repeated numbers are not allowed. Please try again");
                            }
                            

                        }
                        if (!range.Contains(Convert.ToInt32(userValue)))
                        {
                            Console.WriteLine("Invalid input. Please enter a number betweeon 0 and 20");
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
            SQLclass.DBplayerInsert();
            if (SQLclass.Duplicate == -1)
            {
                Console.WriteLine("You already entered this lottery, please wait for a new one");
            }
            else
            {
                Program.client.ExecuteClient();
            }
        }
    }
}
