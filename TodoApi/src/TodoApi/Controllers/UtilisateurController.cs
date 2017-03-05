using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
   // [ApiAuthentificationFilter(true)]  --> filters
    public class UtilisateurController : Controller
    {
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

        // POST api/utilisateur/add?name=untel&mail=jjjj&mdp=ddddd
        [HttpPost("add")]
        public string Post(string name, string mail, string mdp)
        {
            UtilisateurContextFactory u = new UtilisateurContextFactory();
            var user = new Utilisateur() { Name = name, Mail = mail, Mdp = mdp };
          //  user.Name = name; user.Mail = mail; user.Mdp = mdp;
            u.Add(user);
            return user.Mdp + " a été ajouté";
        }

        // PUT api/utilisateur/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
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
                return name + "name est null";
        }
    }
}
