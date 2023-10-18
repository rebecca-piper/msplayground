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
        double pot;
        int userStake;
        int[] stakeArr = new int[5];
        public string Playerusername { get => playerusername; set => playerusername = value; }
        public double Pot { get => pot; set => pot = value; }
        public int UserStake { get => userStake; set => userStake = value; }
        public int[] StakeArr { get => stakeArr; set => stakeArr = value; }

     

        public void PlayerName()
        {
            if (!string.IsNullOrEmpty(Playerusername))
                return;

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
        public void Stake()
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
        public override string ToString()
        {
            return $"name:{playerusername} pot:{pot} userstake:{userStake} stakearr:{{{ String.Join(",",stakeArr) }}}";
        }

    }
}
