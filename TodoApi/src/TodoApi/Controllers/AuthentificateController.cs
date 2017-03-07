
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using System.Security.Cryptography;
using MySql.Data.MySqlClient.Framework.NetCore10;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthentificateController : Controller
    {
        //http://localhost:51103/api/authentificate/auth?mail=workshopp@gmail.com&mdp=pwd
        [HttpPost("auth")]
        public IEnumerable<string> Post(string mail, string mdp)
        {
            UtilisateurContextFactory u = new UtilisateurContextFactory();
            string token = u.Authentificate(mail, mdp);
            List<string> couple = new List<string>();
            couple.Add(mail);
            couple.Add(token);
            return couple; //mail et tokenId
        }
    }
}
