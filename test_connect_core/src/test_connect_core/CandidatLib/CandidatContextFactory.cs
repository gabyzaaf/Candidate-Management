using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_connect_core
{
    public class CandidatContextFactory
    {
        public static CandidatContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CandidatContext>();
            optionsBuilder.UseMySQL(connectionString);

            var context = new CandidatContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

            return context;
        }


        public static IOrderedQueryable<Candidat> ExecuteSqlQuery(string connectionString, IOrderedQueryable sql)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CandidatContext>();
            optionsBuilder.UseMySQL(connectionString);

            var context = new CandidatContext(optionsBuilder.Options);
            var query = from b in context.candidats
                        orderby b.Name
                        select b;

            return query;
        }
    }
}
