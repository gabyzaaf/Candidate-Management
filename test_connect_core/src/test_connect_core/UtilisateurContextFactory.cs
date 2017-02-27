using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public static UtilisateurContext Authentifcation(string connectionString, string sql)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UtilisateurContext>();
            optionsBuilder.UseMySQL(connectionString);

            var context = new UtilisateurContext(optionsBuilder.Options);
            context.Database.ExecuteSqlCommand(sql);

            return context;
        }
    }
}
