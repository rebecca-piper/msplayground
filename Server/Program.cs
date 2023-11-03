using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace Server
{
    internal class Program
    {
        private static Lottery lottery = new Lottery();
        public static Lottery Lottery { get => lottery; set => lottery = value; }
        static async void Main(string[] args)
        {
            ServerSetup serversetup = new ServerSetup();           
            lottery.SetTimer();
            serversetup.ExecuteServer();
        }
    }
}