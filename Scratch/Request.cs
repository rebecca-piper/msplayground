using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scratch
{
    internal class Request
    {
        public int Balance;
        public int Stake;

        public void GetRequest()
        {
            Stake = 5;
            Balance = Balance - Stake;
        }
    }
}
