using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class ClientVars
    {
        int userStake;
        int[] userNums;
        string playerusername;

   
        public string Playerusername { get => playerusername; set => playerusername = value; }
        public int Userstake { get => userStake; set => userStake = value; }
        public int[] UserNums { get => userNums; set => userNums = value; }
    }
}
