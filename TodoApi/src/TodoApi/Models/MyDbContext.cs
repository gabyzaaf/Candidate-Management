using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        //ma bdd
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        { }
        //mes tables
        public DbSet<Candidat> candidats { get; set; }
        public DbSet<Utilisateur> utilisateurs { get; set; }
    }
}
