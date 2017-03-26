using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Xml;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace TodoApi.Models
{
    public class UtilisateurContextFactory
    {
        public MyDbContext _context;

        public UtilisateurContextFactory()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("sqlsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("SampleConnection");
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseMySQL(connectionString);
            _context = new MyDbContext(optionsBuilder.Options);
            _context.Database.EnsureCreated();
        }


        #region methodes utiles
 
        //Obtenir la liste de tout les utilisateurs
        public IEnumerable<Utilisateur> GetAll()
        {
            return _context.utilisateurs.ToList();
        }

        //recherche d'un utilisateur par son nom
        public Utilisateur FindByName(string name)
        {
            return _context.utilisateurs.FirstOrDefault(u => u.Name == name);
        }

        //Obtenir utilisateur avec nom ET mdp
        public Utilisateur Get(string mail, string pass)
        {
            return _context.utilisateurs.FirstOrDefault(u => u.Mail == mail && u.Mdp == pass);
        }

        //Obtenir le mdp en base de l'utilisateur
        public string GetPwd(string mail)
        {
            Utilisateur ut = new Utilisateur();
            ut = _context.utilisateurs.FirstOrDefault(u => u.Mail == mail);
            if (ut != null)
                return ut.Mdp;
            else
                return "Mail erroné";
        }

        //Obtenir le mdp en base de l'utilisateur /!\ pour les tests
        public string GetPwdHash(string mdp)
        {
           return Utils.CreateMD5(mdp);
        }

        //recherche d'un utilisateur par son id
        public Utilisateur FindById(int _id)
        {
            return _context.utilisateurs.FirstOrDefault(u => u.Id == _id);
        }


        #endregion

        #region Gestion user
        public string Authentificate(string mail, string password)
        {

            string mdpBdd = GetPwd(mail);
            if (mdpBdd != null)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    if (Utils.VerifyMd5Hash(md5Hash, mdpBdd, password))
                    {
                        return "Connexion réussie";
                    }

                    else
                    {
                        return "Mot de passe incorrecte";
                    }
                }
            }
            else
               return "Une erreur de recherche est survenue";
        }

        //Inscrire un nouvel utilisateur

        public void Inscription(Utilisateur user)
        {
            _context.utilisateurs.Add(user);
            _context.SaveChanges();
        }

        //supprimer utilisateur en utilisant son nom
        public void RemoveByName(string name)
        {
            var entity = _context.utilisateurs.First(u => u.Name == name);
            _context.utilisateurs.Remove(entity);
            _context.SaveChanges();
        }

        //Modifier un utilisateur
        public void Update(Utilisateur utilisateur)
        {
            _context.utilisateurs.Update(utilisateur);
            _context.SaveChanges();
        }

        //Ajouter un utilisateur
        public void Add(Utilisateur utilisateur)
        {
            _context.utilisateurs.Add(utilisateur);
            _context.SaveChanges();
        }

        #endregion

    }
}
