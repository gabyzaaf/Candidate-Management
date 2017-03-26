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
    [Route("api/[controller]")]
    public class CandidatController : Controller
    {

        #region ws utils
        [HttpGet]
        public IEnumerable<Candidat> GetAll()
        {
            CandidatContextFactory u = new CandidatContextFactory();
            return u.GetAll();
        }

        //api/Candidat/byId?id=1
        [HttpGet("byName")]            //par id/name/autres champs.. au choix
        public string Get(string name) 
        {
            CandidatContextFactory u = new CandidatContextFactory();
            Candidat user = new Candidat();
            user = u.GetByName(name);
            if (user != null)
                return user.Name;      // Sortie attendu coté view ? 
            else
                return "Candidat introuvable";

        }

        // POST api/candidat/add?name=untel&lastname=jean&mail=jjjj@gmail.com
        [HttpPost("add")]
        public string Post(string name, string mail, string lastname)
        {
            CandidatContextFactory u = new CandidatContextFactory();
            var cand = new Candidat() { Name = name, LastName = lastname, Mail = mail };

            u.Add(cand);
            return cand.Name+" "+cand.LastName + " a été ajouté";
        }

        #endregion
    }
}
