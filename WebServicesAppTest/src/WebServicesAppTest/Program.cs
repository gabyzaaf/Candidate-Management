using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebServicesAppTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("coucou");
            Console.ReadLine();

            /****** Connexion BDD **************/

            MySqlConnection connection = new MySqlConnection
            {
                ConnectionString = "server=<ServerAddress>;user id=<User>;password=<Password>;persistsecurityinfo=True;port=<Port>;database=sakila"
            };
            connection.Open();

            /*********Requetage *************/

            MySqlCommand command = new MySqlCommand("SELECT * FROM sakila.category;", connection);

            /******* Execution Requetage ***********/

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                System.Console.WriteLine("Category Id\t\tName\t\tLast Update");
                while (reader.Read())
                {
                    string row = $"{reader["category_id"]}\t\t{reader["name"]}\t\t{reader["last_update"]}";
                    System.Console.WriteLine(row);
                }
            }

            connection.Close();

            System.Console.ReadKey();
        }
    }
}
