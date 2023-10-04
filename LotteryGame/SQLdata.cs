using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame
{
    public class SQLdata
    {
        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        string playerusername;
        int prizes;
        int[] usernumbers = new int[6];
        int[] randomnumbers = new int[6];
        public string Playerusername { get => playerusername; set => playerusername = value; }
        public int Prizes { get => prizes; set => prizes = value; }
        public int[] Usernumbers { get => usernumbers; set => usernumbers = value; }
        public int[] Randomnumbers { get => randomnumbers; set => randomnumbers = value; }

        public static void Builder()
        {
            builder.DataSource = "TOMBOLA-1665";
            builder.InitialCatalog = "test";
            builder.IntegratedSecurity = true;
            builder.TrustServerCertificate = true;
        }
        
        public void DBplayerInsert(string pPlayerusername)
        {
            playerusername = pPlayerusername;
            Console.WriteLine("PLease enter your username");
             pPlayerusername = Console.ReadLine();
            Builder();
            try
            {
                bool isValidInput = false;
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    while (!isValidInput)
                    {


                        string duplicateName = "SELECT player_username FROM player WHERE player_username = '" + pPlayerusername + "'";

                        using (SqlCommand command = new SqlCommand(duplicateName, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {


                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    pPlayerusername = reader.GetString(0);
                                    Console.WriteLine(pPlayerusername + "is already used, please choose a different username");
                                    pPlayerusername = Console.ReadLine();
                                    connection.Close();
                                }
                                else
                                {
                                    connection.Close();

                                    string sqlplayer = "INSERT INTO player (player_username) VALUES ('" + pPlayerusername + "')";
                                    connection.Open();
                                    using (SqlCommand cmd = new SqlCommand(sqlplayer, connection))
                                    {


                                        cmd.ExecuteNonQuery();

                                        Console.WriteLine("Successfully inserted in player");
                                        break;
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void DBgameinsert(int[] pUsernumbers, int[] pRandomNumbers, string pPlayerusername, int pPrizes)
        {
            usernumbers = pUsernumbers;
            randomnumbers = pRandomNumbers;
            prizes = pPrizes;
            Builder();
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    string IDquery = "SELECT player_id FROM player WHERE player_username = '" + pPlayerusername + "'";
                    decimal playerid = 0;
                    using (SqlCommand command = new SqlCommand(IDquery, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                reader.Read();
                                playerid = reader.GetDecimal(0);

                            }

                        }
                    }
                    string sqlgame = "INSERT INTO games (player_id, ticket, player_numbers, prizes) VALUES ('" + playerid + "' ,@ticket, @playernumbers, '" + pPrizes + "')";
                    using (SqlCommand cmd = new SqlCommand(sqlgame, connection))
                    {
                        cmd.Parameters.AddWithValue("ticket", string.Join(",", pRandomNumbers));
                        cmd.Parameters.AddWithValue("playernumbers", string.Join(",", pUsernumbers));
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Successfully inserted in game");
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
            Builder();
            try
            {
                decimal playerid = 0;
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();
                    string IDquery = "SELECT player_id FROM player WHERE player_username = '" + pPlayerusername + "'";

                    using (SqlCommand command = new SqlCommand(IDquery, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                reader.Read();
                                playerid = reader.GetDecimal(0);
                                connection.Close();

                            }
                            else
                            {
                                Console.WriteLine("Player not found");
                            }

                        }
                    }

                }
                using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    string sGames = "SELECT game_id, ticket, player_numbers FROM games WHERE player_id = '" + playerid + "'";

                    using (SqlCommand cmd = new SqlCommand(sGames, conn))
                    {

                        using (SqlDataReader readergame = cmd.ExecuteReader())
                        {
                            if (readergame.HasRows)
                            {

                                readergame.Read();
                                decimal gameID = readergame.GetDecimal(0);
                                string ticket = readergame.GetString(1);
                                string playernumbers = readergame.GetString(2);
                                Console.WriteLine("Game ID:" + gameID);
                                Console.WriteLine("Ticket:" + ticket);
                                Console.WriteLine("Numbers:" + playernumbers);

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
