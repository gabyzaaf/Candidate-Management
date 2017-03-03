using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCandidat
{
    public class Authentification
    {
        public string email { get; set; }
        public string password { get; set; }

        public Authentification(string aemail, string apassword)
        {
            email = aemail;
            password = apassword;
        }
    }
}