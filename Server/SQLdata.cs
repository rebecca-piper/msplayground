using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Data.SqlClient;

namespace Server
{
    public class SQLdata
    {
        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
        {
            DataSource = "localhost",
            InitialCatalog = "test",
            IntegratedSecurity = true,
            TrustServerCertificate = true
        };

        int[] randomnumbers = new int[6];
        string calls;
        public int[] callsArr = new int[6];
        public int[] picksArr = new int[6];
        double storedPot;
 
        public int[] Randomnumbers { get => randomnumbers; set => randomnumbers = value; }
        public string Calls { get => calls; set => calls = value; }
        public int[] CallsArr { get => callsArr; set => callsArr = value; }
        public double StoredPot { get => storedPot; set => storedPot = value; }

        //public void NewLotteryInsert(int[] pUsernumbers, int[] pRandomNumbers, double pPrizes, double pPot)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        //        {
        //            using (SqlCommand command = new SqlCommand("dbo.lotteryproc", connection))
        //            {
        //                connection.Open();
        //                command.Parameters.AddWithValue("@calls", string.Join(",", pRandomNumbers));
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.ExecuteNonQuery();

        //            }
        //            using (SqlCommand command = new SqlCommand("dbo.gamesproc", connection))
        //            {
        //                //command.Parameters.AddWithValue("@player_username", Game.PlayerClass.Playerusername);
        //                command.Parameters.AddWithValue("@picks", string.Join(",", pUsernumbers));
        //                command.Parameters.AddWithValue("@prizes", pPrizes);
        //                command.Parameters.AddWithValue("@pot", pPot);
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.ExecuteNonQuery();

        //            }

        //        }
        //    }
        //    catch (SqlException e)
        //    {
        //        Console.WriteLine("SQL error in inserting game" + e.ToString());
        //    }
        //    catch (InvalidOperationException e)
        //    {
        //        Console.WriteLine("Connection error in inserting game" + e);
        //    }
        //}
        public async Task DBgameinsert()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("dbo.gamesproc", connection))
                    {
                        command.Parameters.AddWithValue("@player_username", ServerSetup.client.Playerusername);
                        command.Parameters.AddWithValue("@picks", string.Join(",", ServerSetup.client.UserNums));
                        command.Parameters.AddWithValue("@prizes", Program.Lottery.Prize);
                        command.CommandType = CommandType.StoredProcedure;
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("SQL error in inserting game" + e.ToString());
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Connection error in inserting game" + e);
            }
        }
        public async Task GetExistingGame()
        {    
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("dbo.playexistinggame", connection))
                    {
                        command.Parameters.Add("@lottery_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@calls", SqlDbType.VarChar, 30).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@pot_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@pot", SqlDbType.Int).Direction = ParameterDirection.Output;
                        command.CommandType = CommandType.StoredProcedure;
                        await command.ExecuteNonQueryAsync();
                        //lotteryID = (int)command.Parameters["@lottery_id"].Value;
                        calls = (string)command.Parameters["@calls"].Value;
                        storedPot = (int)command.Parameters["@pot"].Value;
                        //Console.WriteLine("Existing game: " + lotteryID);
                        string[] s1 = calls.Split(',');
                        callsArr = Array.ConvertAll(s1, n => int.Parse(n));
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("SQL error in pulling game" + e.ToString());
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Connection error in pulling game" + e);
            }
        }

        public async Task UpdatePot()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("dbo.potupdate", connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@pot", Program.Lottery.CurrentPot);
                        command.CommandType = CommandType.StoredProcedure;
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("SQL error in updating pot" + e.ToString());
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Connection error in updating pot" + e);
            }
        }
        public async Task NewLotteryTimer(int[] pRandomNumbers)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand("dbo.lotteryproc", connection))
                        {
                            connection.Open();
                            command.Parameters.AddWithValue("@calls", string.Join(",", pRandomNumbers));
                            command.CommandType = CommandType.StoredProcedure;
                            await command.ExecuteNonQueryAsync();
                        }
                    }                   
                    catch (SqlException e)
                    {
                        Console.WriteLine("SQL error in inserting lottery" + e.ToString());
                    }
                    try
                    {
                        using (SqlCommand command = new SqlCommand("dbo.potproc", connection))
                        {
                            command.Parameters.AddWithValue("@pot", 0);
                            command.CommandType = CommandType.StoredProcedure;
                            await command.ExecuteNonQueryAsync();
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("SQL error in inserting pot" + e.ToString());
                    }
                }
            }          
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Connection error in inserting lottery" + e);
            }
        }
    }
}
