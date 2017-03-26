using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Candidat
    {
        public Candidat() { }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }

    }
}
