using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_connect_core
{

    using Microsoft.EntityFrameworkCore;

    public class CandidatContext : DbContext
    {
        public CandidatContext(DbContextOptions<CandidatContext> options) : base(options)
        { }
        public DbSet<Candidat> candidats { get; set; }
    }
}
