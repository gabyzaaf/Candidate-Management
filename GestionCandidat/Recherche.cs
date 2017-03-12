using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCandidat
{
    public class Recherche
    {
        public string nom { get; set; }
        public string prenom { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public string token { get; set; }

        public Recherche(string rnom, string rprenom, string rtelephone, string remail, string rtoken)
        {
            nom = rnom;
            prenom = rprenom;
            telephone = rtelephone;
            email = remail;
            token = rtoken;
        }
    }
}