using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml;

namespace test_connect_core
{
    public class UtilisateurContextFactory
    {
        public static UtilisateurContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UtilisateurContext>();
            optionsBuilder.UseMySQL(connectionString);

            var context = new UtilisateurContext(optionsBuilder.Options);
            context.Database.EnsureCreated();
            
            return context;
        }

        public static IOrderedQueryable<Utilisateur> ExecuteSqlQuery(string connectionString, IOrderedQueryable sql)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UtilisateurContext>();
            optionsBuilder.UseMySQL(connectionString);

            var context = new UtilisateurContext(optionsBuilder.Options);
            var query = from b in context.Utilisateurs
                        orderby b.Name
                        select b;

            return query;
        }
    }
}
