using System;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net.Mail;
namespace sqltest
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Welcome to the Customer Form");
            Console.WriteLine("-----------------------------");

            string sName;
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            string sEmail;
            int iNum;
            string sPostcode;
            try
            {
                Console.WriteLine("Please enter your Full Name");
                sName = Console.ReadLine();
                Console.WriteLine("please enter your email");
                sEmail = Console.ReadLine();
                while (!IsValidEmail(sEmail, pattern))
                {
                    Console.WriteLine("Invalid email format. Please try again");
                    sEmail = Console.ReadLine();
                }

                Console.WriteLine("please enter your telephone number");

                //bool parsedSuccessfully = int.TryParse(Console.ReadLine(), out iNum);

                //    if(parsedSuccessfully == false)
                //    {
                //        Console.WriteLine("Please enter a number");

                //    }

                bool isValidInput = false;

                while (!isValidInput)
                {
                    Console.Write("Enter an integer: ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out iNum))
                    {
                        isValidInput = true;
                        Console.WriteLine($"You entered: {iNum}");

                        Console.WriteLine("please enter your postcode");
                        sPostcode = Console.ReadLine();


                        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                        builder.DataSource = "TOMBOLA-1665";
                        builder.InitialCatalog = "test";
                        builder.IntegratedSecurity = true;
                        builder.TrustServerCertificate = true;
                        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                        {
                            string sql = "INSERT INTO customer (cust_name, cust_email, cust_number, cust_postcode) VALUES ('" + sName + "',' " + sEmail + "', ' " + iNum + " ' , '" + sPostcode + "')";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                connection.Open();

                                command.ExecuteNonQuery();
                                Console.WriteLine("Successfully inserted");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer.");
                    }
                }




            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        static bool IsValidEmail(string sEmail, string pattern)
        {
            return Regex.IsMatch(sEmail, pattern);
        }

    }

}

