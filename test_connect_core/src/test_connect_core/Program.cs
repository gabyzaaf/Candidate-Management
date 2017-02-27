using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_connect_core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Récupérer les bons settings json
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("SampleConnection");

         
            //Créer un utilisateur
            var entry = new Utilisateur() { Name = "Julie", Mail = "julieG@gmail.com" };

            using (var context = UtilisateurContextFactory.Create(connectionString))
            {
                context.Add(entry);
                context.SaveChanges();
            }

            Console.WriteLine("Utilisateur créer en DB avec l'id : {entry.Id}");
            Console.Read();

        }
    }
}
