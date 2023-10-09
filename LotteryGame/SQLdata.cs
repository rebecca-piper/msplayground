using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame
{
    public class SQLdata
    {
        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        string playerusername;

        int[] usernumbers = new int[6];
        int[] randomnumbers = new int[6];
        public string Playerusername { get => playerusername; set => playerusername = value; }

        public int[] Usernumbers { get => usernumbers; set => usernumbers = value; }
        public int[] Randomnumbers { get => randomnumbers; set => randomnumbers = value; }

        public static void Builder()
        {
            builder.DataSource = "localhost";
            builder.InitialCatalog = "test";
            builder.IntegratedSecurity = true;
            builder.TrustServerCertificate = true;
        }

        public void DBplayerInsert(string pPlayerusername)
        {
           

            Builder();
            bool isValidInput = false;
            
            try
            {
                while (!isValidInput)
                {
                    Console.WriteLine("Please enter a username");
                    playerusername = Console.ReadLine();
                    int duplicate = 0;
                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {  
                        using (SqlCommand command = new SqlCommand("dbo.playerproc", connection))
                    {
                            connection.Open();
                            //command.Parameters.Add(new SqlParameter("@player_username", SqlDbType.VarChar, 30)).Value = playerusername;
                            command.Parameters.AddWithValue("@player_username", playerusername);
                            command.Parameters.Add("@duplicate", SqlDbType.Int).Direction = ParameterDirection.Output;
                            command.CommandType = CommandType.StoredProcedure;
                            command.ExecuteNonQuery();
                            duplicate = (int)command.Parameters["@duplicate"].Value;
                        }
                    }
                    if (duplicate == -1)
                    {
                        Console.WriteLine("Username is already used, please try again");


                    }
                    else
                    {
                        break;
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void DBgameinsert(int[] pUsernumbers, int[] pRandomNumbers, int pPrizes)
        {
            usernumbers = pUsernumbers;
 
            Builder();
           
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("dbo.lotteryproc", connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@calls", string.Join(",", pRandomNumbers));
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();

                    }
                    using (SqlCommand command = new SqlCommand("dbo.gamesproc", connection))
                    {
                        command.Parameters.AddWithValue("@player_username", playerusername);
                        command.Parameters.AddWithValue("@picks", string.Join(",", pUsernumbers));
                        command.Parameters.AddWithValue("@prizes", pPrizes);
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();

                    }
                   

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void PreviewGames(string pPlayerusername)
        {
            var gameID = 0;
            Builder();
            try
            {
                
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();
              

                    using (SqlCommand command = new SqlCommand("dbo.previewgame", connection))
                    {
                        command.Parameters.AddWithValue("@player_username", playerusername);
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                               while (reader.Read()) 
                               {
                                    gameID = Convert.ToInt32(reader["player_id"]);
                                    Console.WriteLine(gameID);
                                    
                                }
                                
                                
                                connection.Close();

                            }
                            else
                            {
                                Console.WriteLine("Player not found");
                            }

                        }
                    }

                }
                //using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
                //{
                //    conn.Open();
                //    string sGames = "SELECT game_id, picks FROM games WHERE player_id = '" + playerid + "'";

                //    using (SqlCommand cmd = new SqlCommand(sGames, conn))
                //    {

                //        using (SqlDataReader readergame = cmd.ExecuteReader())
                //        {
                //            if (readergame.HasRows)
                //            {

                //                readergame.Read();
                //                decimal gameID = readergame.GetDecimal(0);
                //                string ticket = readergame.GetString(1);
                //                string playernumbers = readergame.GetString(2);
                //                Console.WriteLine("Game ID:" + gameID);
                //                Console.WriteLine("Ticket:" + ticket);
                //                Console.WriteLine("Numbers:" + playernumbers);

                //            }
                //        }
                //    }
                //}
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void ExistingGame()
        {
            Builder();
            decimal gameID = 0;
            try

            {

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();
                    string IDquery = "SELECT lottery_id FROM games WHERE game_id = (SELECT max(game_id) FROM games)";

                    using (SqlCommand command = new SqlCommand(IDquery, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                reader.Read();
                                decimal lotteryID = reader.GetDecimal(0);
                                string calls = reader.GetString(1);
                                Console.WriteLine("Existing game: " + lotteryID);
                                Console.WriteLine("calls : " + calls);
                                connection.Close();

                            }
                            else
                            {
                                Console.WriteLine("Player not found");
                            }

                        }
                    }

                }
            }
            catch (SqlException e)
            {

            }
        }
    }
}
