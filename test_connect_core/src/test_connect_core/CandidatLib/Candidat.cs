using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_connect_core
{
    public class Candidat
    {
        public Candidat()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public bool toBeCalled { get; set; }
    }
}
