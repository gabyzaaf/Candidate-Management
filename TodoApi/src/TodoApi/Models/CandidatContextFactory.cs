using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Xml;
using Microsoft.Extensions.Configuration;

namespace TodoApi.Models
{
    public class CandidatContextFactory
    {
        public MyDbContext _context;

        public CandidatContextFactory()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("sqlsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("SampleConnection");
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseMySQL(connectionString);
            _context = new MyDbContext(optionsBuilder.Options);
            _context.Database.EnsureCreated();
        }

        #region Méthodes utiles

        //Obtenir la liste de candidat
        public IEnumerable<Candidat> GetAll()
        {
          return  _context.candidats.ToList();
        }

        //Rechercher un candidat par son nom
        public Candidat GetByName(string name)
        {
            return _context.candidats.FirstOrDefault(s => s.Name == name);
        }

        #endregion

        #region Gestion Candidat

        //Ajouter une entité candidat
        public void Add(Candidat candidat)
        {
            _context.candidats.Add(candidat);
            _context.SaveChanges();
        }

        #endregion
    }
}
