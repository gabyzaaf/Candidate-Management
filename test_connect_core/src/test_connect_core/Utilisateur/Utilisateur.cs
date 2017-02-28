using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_connect_core
{
    public class Utilisateur
    {
        public Utilisateur()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Mdp { get; set; }
    }
}
