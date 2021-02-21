using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIHTest.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public class UserReponse
    {
        public UserReponse(User user, string token)
        {
            this.UserId = user.UserId;
            this.Token = token;
        }
        public string UserId { get; set; }

        public string Token { get; set; }
    }
}
