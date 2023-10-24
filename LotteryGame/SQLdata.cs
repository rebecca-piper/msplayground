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
        public void DBplayerInsert()
        {

            bool isValidInput = false;

            while (!isValidInput)
            {
                Program.Player.GetPlayerName();
                int duplicate = 0;
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
                    if (duplicate == -1)
                    {

                        Console.WriteLine("Username is already used, please try again");


                    }
                    else
                    {
                        break;
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

        }

    }
}
