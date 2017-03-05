using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public interface IUserServices
    {
        int Authentificate(string userName, string password);
    }
}
