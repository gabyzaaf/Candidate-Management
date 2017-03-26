using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Security.Cryptography;
using MySql.Data.MySqlClient.Framework.NetCore10;
using Microsoft.AspNetCore.Authorization;

namespace TodoApi.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
   // [ApiAuthentificationFilter(true)]  --> filters
   // [AutoValidateAntiforgeryToken]
    public class UtilisateurController : Controller
    {
        #region ws utils
        // GET api/Utilisateur
        [HttpGet]
        public IEnumerable<Utilisateur> GetAll()
        {
            UtilisateurContextFactory u = new UtilisateurContextFactory();
            return u.GetAll();
        }

        //api/Utilsateur/byId?id=1
        [HttpGet("byId")]
        public string Get(int id)
        {
            UtilisateurContextFactory u = new UtilisateurContextFactory();
            Utilisateur user = new Utilisateur();
            user = u.FindById(id);
            if (user != null)
                return user.Name;
            else
                return "Utilisateur introuvable";
        }

        // DELETE api/utilisateur/delete?name=
        [HttpDelete("delete")]
        public string Delete(string name)
        {
            UtilisateurContextFactory u = new UtilisateurContextFactory();
        
            if (name != null)
            {
                u.RemoveByName(name);
                return name + " supprimé ";
            }
            else
                return name + "L'utilisateur n'existe pas";
        }

        // GET api/utilisateur/testmdp?mdp=secret
        [HttpGet("testmdp")]
        public string TestMDP(string mdp)
        {
            UtilisateurContextFactory u = new UtilisateurContextFactory();
            return u.GetPwdHash(mdp);
        }

        #endregion

        #region Gestion user
        // AUTHENTIFICATION  
        // GET api/utilisateur/Auth?mail=gamelinfabien@gail.com&mdp=kojceoleo8KFKJFEfeE       (attention mdp crypté en entrée)
        [HttpGet("Auth")]
        public string Autentification(string mail, string mdp)
        {
            UtilisateurContextFactory u = new UtilisateurContextFactory();
            return u.Authentificate(mail, mdp);
        }

        //INSCRIPTION
        // POST api/utilisateur/inscription?name=untel&lastname=truc&mail=jjjj&mdp=ddddd
        [HttpPost("inscription")]
        public string Inscription(string name, string mail, string mdp)
        {
            UtilisateurContextFactory u = new UtilisateurContextFactory();

            var user = new Utilisateur() { Name = name, Mail = mail, Mdp = mdp };

            u.Add(user);
            return " User : " + user.Name + " mot de passe : " + mdp;
        }
        #endregion
    }
}
