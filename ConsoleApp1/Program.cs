using System;
using Microsoft.Data.SqlClient;
using System.Text;

namespace sqltest
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Welcome to the Customer Form");
            Console.WriteLine("-----------------------------");

            string sName;
            string sEmail;
            int iNum;
            string sPostcode;

            Console.WriteLine("Please enter your Full Name");
            sName = Console.ReadLine();
            Console.WriteLine("please enter your email");
            sEmail = Console.ReadLine();
            Console.WriteLine("please enter your telephone number");
            iNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("please enter your postcode");
            sPostcode = Console.ReadLine();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "TOMBOLA-1665";
                builder.InitialCatalog = "test";
                builder.IntegratedSecurity = true;
                builder.TrustServerCertificate = true;
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    string sql = "INSERT INTO customer (cust_name, cust_email, cust_number, cust_postcode) VALUES ('" +sName + "',' " + sEmail + "', ' " + iNum + " ' , '" + sPostcode + "')";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        //using (SqlDataReader reader = command.ExecuteReader())
                        //{
                        //    while (reader.Read())
                        //    {
                        //        Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetDecimal(1));
                        //    }
                        //}
                        command.ExecuteNonQuery();
                        Console.WriteLine("Successfully inserted");
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

