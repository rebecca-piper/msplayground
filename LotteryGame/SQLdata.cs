﻿using Microsoft.Data.SqlClient;
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
          Player playerClass = new Player();
               
        int[] usernumbers = new int[6];
        int[] randomnumbers = new int[6];
        string calls;
        public int[] callsArr = new int[6];
        public int[] picksArr = new int[6];
        double storedPot;

        public int[] Usernumbers { get => usernumbers; set => usernumbers = value; }
        public int[] Randomnumbers { get => randomnumbers; set => randomnumbers = value; }
        public string Calls { get => calls; set => calls = value; }
        public int[] CallsArr { get => callsArr; set => callsArr = value; }
        public double StoredPot { get => storedPot; set => storedPot = value; }

        public static void Builder()
        {
            builder.DataSource = "localhost";
            builder.InitialCatalog = "test";
            builder.IntegratedSecurity = true;
            builder.TrustServerCertificate = true;
        }

        public void DBplayerInsert()
        {
            
            Builder();
            bool isValidInput = false;

                while (!isValidInput)
                {
                playerClass.PlayerName();
                    int duplicate = 0;
                try 
                { 
                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {  
                        using (SqlCommand command = new SqlCommand("dbo.playerproc", connection))
                    {
                            connection.Open();
                            //command.Parameters.Add(new SqlParameter("@player_username", SqlDbType.VarChar, 30)).Value = playerusername;
                            command.Parameters.AddWithValue("@player_username", playerClass.Playerusername);
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
                    Console.WriteLine("SQL error in inserting player"+ e.ToString());
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("Connection error in inserting player" + e);
                }
        }
          

        }

        public void NewLotteryInsert(int[] pUsernumbers, int[] pRandomNumbers, double pPrizes )
        {
            Lottery lottpot = new Lottery();
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
                        command.Parameters.AddWithValue("@player_username", playerClass.Playerusername);
                        command.Parameters.AddWithValue("@picks", string.Join(",", pUsernumbers));
                        command.Parameters.AddWithValue("@prizes", pPrizes);
                        command.Parameters.AddWithValue("@pot", lottpot.Pot);
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();

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
        public void DBgameinsert(int[] pUsernumbers, int[] pRandomNumbers, double pPrizes )
        {
            Lottery lottpot = new Lottery();
            usernumbers = pUsernumbers;
            
            Builder();
           
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
             
                    using (SqlCommand command = new SqlCommand("dbo.gamesproc", connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@player_username", playerClass.Playerusername);
                        command.Parameters.AddWithValue("@picks", string.Join(",", pUsernumbers));
                        command.Parameters.AddWithValue("@prizes", pPrizes);
                        command.Parameters.AddWithValue("@pot", lottpot.Pot);
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();

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
        public void PreviewGames()
        {
            int playerID = 0;
            int lotteryID = 0;
            string picks = "";
            int prizes;
            Builder();
            try
            {
                
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("dbo.previewgame", connection))
                    {
                        command.Parameters.AddWithValue("@player_username", playerClass.Playerusername);
                        command.Parameters.Add("@player_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@lottery_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@picks", SqlDbType.VarChar, 30).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@prizes", SqlDbType.Int).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@calls", SqlDbType.VarChar, 30).Direction = ParameterDirection.Output;
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();
                       
                            playerID = (int)command.Parameters["@player_id"].Value;
                            lotteryID = (int)command.Parameters["@lottery_id"].Value;
                            picks = (string)command.Parameters["@picks"].Value;
                            string[] s1 = calls.Split(',');
                            picksArr = Array.ConvertAll(s1, n => int.Parse(n));
                            prizes = (int)command.Parameters["@prizes"].Value;
                            Console.WriteLine(playerID.ToString());
                            Console.WriteLine(lotteryID.ToString());
                            Console.WriteLine(picks.ToString());
                            Console.WriteLine(prizes.ToString());
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

        public void ExistingGame()
        {
            Builder();
            var lotteryID = 0;
            
            try

            {

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("dbo.playexistinggame", connection))
                    {
                        command.Parameters.Add("@lottery_id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@calls", SqlDbType.VarChar, 30).Direction = ParameterDirection.Output;
                        command.Parameters.Add("@pot", SqlDbType.Float).Direction = ParameterDirection.Output;
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();
                        //lotteryID = (int)command.Parameters["@lottery_id"].Value;
                        calls = (string)command.Parameters["@calls"].Value;
                        storedPot = (double)command.Parameters["@pot"].Value;
                        //Console.WriteLine("Existing game: " + lotteryID);
                        string[] s1 = calls.Split(','); 
                        callsArr = Array.ConvertAll(s1, n => int.Parse(n));
                          //lotteryID = (int)command.Parameters["@lottery_id"].Value;
                           //Console.WriteLine("Existing game: " + lotteryID);
                              
                                connection.Close();
                        
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
    }
}
