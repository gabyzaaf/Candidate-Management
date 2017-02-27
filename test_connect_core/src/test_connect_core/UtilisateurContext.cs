using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_connect_core
{
    using Microsoft.EntityFrameworkCore;

    public class UtilisateurContext : DbContext
    {
        public UtilisateurContext(DbContextOptions<UtilisateurContext> options) : base(options)
        { }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}
