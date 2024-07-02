using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Globals;

namespace Order_Consumer
{
    public class OrderConsumerMain
    {
        
        private static string connectionString = "Server=localhost;Integrated Security=true;";
        private static string databaseName = "db_DJATESTEN";
        private static string connectionStringDatabase = connectionString + $"Database={databaseName}";
        private static string directory = @"C:\Users\comma\Documents\Orders";

        static void Main(string[] args)
        {
            CreateDatabase();
            CreateTables();

            if (!Directory.Exists(directory))
            {
                Console.WriteLine("Directroy does not exist");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionStringDatabase))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    //Legen van de database zodat er geen dubbele orders in zitten
                    command.CommandText = $"DELETE FROM Orders";
                    command.ExecuteNonQuery();

                    string[] files = Directory.GetFiles(directory);
                    foreach (string file in files)
                    {
                        Globals.Order order = Globals.Order.Deserialize<Order>(file);
                        if(order != null)
                        {
                            int bit = 0;
                            if(order.Status == true)
                            {
                                bit = 1;
                            }
                            else
                            {
                                bit = 0;
                            }
                            command.CommandText = $"INSERT INTO Orders (FileName, Status) VALUES ('{Path.GetFileName(file)}', {bit})";
                            command.ExecuteNonQuery();
                            Console.WriteLine($"{Path.GetFileName(file)} was succesfully added to the database");
                        }
                    }
                }
               
            }
            Console.ReadKey();
        }


        private static void CreateDatabase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{databaseName}') CREATE DATABASE [{databaseName}]";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Database created or already exists.");
                }
                connection.Close();
            }
        }

        private static void CreateTables()
        {
            using (SqlConnection connection = new SqlConnection(connectionStringDatabase))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = 'Orders') CREATE TABLE Orders(FileName char(50),Status bit,TimeStamp DATETIME DEFAULT GETDATE() )";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Table created or already exists.");
                }
                connection.Close();
            }
        }

        
    }
}
