using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class UserServices : IUserServices
    {
        private readonly UtilisateurContextFactory _userFactory;

        public UserServices(UtilisateurContextFactory userFactory)
        {
            _userFactory = userFactory;
        }

        public int Authentificate(string userName, string password)
        {
            var user = _userFactory.Get(userName, password);
            if(user != null && user.Id > 0)
            {
                return user.Id;
            }
            return 0;
        }
    }
}
