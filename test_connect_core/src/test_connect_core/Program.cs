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
            var entry = new Utilisateur() { Name = "rigolus", Mail = "rigolus@gmail.com", Mdp = "pwd" };
            using (var context = UtilisateurContextFactory.Create(connectionString))
            {
                context.Add(entry);   //Nous pouvons supprimer également context.Remove(entry)
                context.SaveChanges();
                Console.WriteLine("Utilisateur crée en DB avec l'id : " + entry.Id);

             //Requete modifiable au choix
                var query = from b in context.Utilisateurs
                            orderby b.Name
                            select b;
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }
            }

            var cand = new Candidat() { Name = "Timothée", LastName = "Dupont", Mail = "timodupont@gmail.com", toBeCalled = false };
            using (var context = CandidatContextFactory.Create(connectionString))
            {
                context.Add(cand);      //Nous pouvons supprimer également context.Remove(cand)
                context.SaveChanges();

                //Requete modifiable au choix
                var query = from b in context.candidats
                            orderby b.Name
                            select b;
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }
            }

                Console.ReadKey(); 
        }
    }
}
