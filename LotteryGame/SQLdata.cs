using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace LotteryGame
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

        private static int[] picksArr = new int[6];
        private static int[] callsArr = new int[6];
        private static string calls;
        private static int playerID;
        private static int lotteryID;
        private static string picks;
        private static int prize;
        private static int duplicate;

        public  int Duplicate { get => duplicate; set => duplicate = value; }
        public  string Calls { get => calls; set => calls = value; }
        public  int PlayerID { get => playerID; set => playerID = value; }
        public int LotteryID { get => lotteryID; set => lotteryID = value; }
        public string Picks { get => picks; set => picks = value; }
        public int Prize { get => prize; set => prize = value; }
        public int[] PicksArr { get => picksArr; set => picksArr = value; }
        public int[] CallsArr { get => callsArr; set => callsArr = value; }

        public void DBplayerInsert()
        {               
                try
                {
                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand("dbo.playerproc", connection))
                        {
                            connection.Open();
                            //command.Parameters.Add(new SqlParameter("@player_username", SqlDbType.VarChar, 30)).Value = playerusername;
                            command.Parameters.AddWithValue("@player_username", Program.Player.Playerusername);
                            command.Parameters.Add("@duplicate", SqlDbType.Int).Direction = ParameterDirection.Output;
                            command.CommandType = CommandType.StoredProcedure;
                            command.ExecuteNonQuery();
                            duplicate = (int)command.Parameters["@duplicate"].Value;
                        }
                    }
                    if (duplicate == -2)
                    {
                        Console.WriteLine("Username is already registered");                   
                    }

                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL error in inserting player" + e.ToString());
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("Connection error in inserting player" + e);
                }
            
        }
        public void PreviewGames()
        {
   
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("dbo.previewgame", connection))
                    {
                        command.Parameters.AddWithValue("@player_username", Program.Player.Playerusername);
                        command.Parameters.Add("@player_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@lottery_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@picks", SqlDbType.VarChar, 30).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@prizes", SqlDbType.Int).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@calls", SqlDbType.VarChar, 30).Direction = ParameterDirection.Output;
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();

                        playerID = (int)command.Parameters["@player_id"].Value;                       
                        picks = (string)command.Parameters["@picks"].Value;
                        prize = (int)command.Parameters["@prizes"].Value;
                        calls = (string)command.Parameters["@calls"].Value;
                        string[] s1 = picks.Split(',');
                        string[] s2 = calls.Split(',');
                        picksArr = Array.ConvertAll(s1, n => int.Parse(n));
                        callsArr = Array.ConvertAll(s2, n => int.Parse(n));
                     
                        connection.Close();

                    }
                }

            }

            catch (SqlException e)
            {
                Console.WriteLine("SQL error in previewing game" + e.ToString());
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Connection error in previewing game" + e);
            }
        }

    }
}
