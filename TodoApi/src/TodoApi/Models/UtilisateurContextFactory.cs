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
    public class UtilisateurContextFactory
    {
        public MyDbContext _context;

        public UtilisateurContextFactory()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("C:\\Users\\gamelinf\\Documents\\Visual Studio 2015\\Dotnetcore\\TodoApi\\src\\TodoApi\\sqlsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("SampleConnection");
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseMySQL(connectionString);
            _context = new MyDbContext(optionsBuilder.Options);
            _context.Database.EnsureCreated();
        }

        public IEnumerable<Utilisateur> GetAll()
        {
            return _context.utilisateurs.ToList();
        }

        public void Add(Utilisateur utilisateur)
        {
            _context.utilisateurs.Add(utilisateur);
            _context.SaveChanges();
        }

        public Utilisateur FindByName(string name)
        {
            return _context.utilisateurs.FirstOrDefault(u => u.Name == name);
        }

        public Utilisateur Get(string name, string pass)
        {
            return _context.utilisateurs.FirstOrDefault(u => u.Name == name && u.Mdp == pass);
        }

        public Utilisateur FindById(int _id)
        {
            return _context.utilisateurs.FirstOrDefault(u => u.Id == _id);
        }

        public void RemoveByName(string name)
        {
            var entity = _context.utilisateurs.First(u => u.Name == name);
            _context.utilisateurs.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Utilisateur utilisateur)
        {
            _context.utilisateurs.Update(utilisateur);
            _context.SaveChanges();
        }
    }
}
