using EIHTest.Interfaces;
using EIHTest.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EIHTest.Helpers
{
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User {  UserId= "test", Password = "test" }
        };

        private readonly AppSettings _appSettings;
        private readonly ILog _log;

        public UserService(IOptions<AppSettings> appSettings,ILog log)
        {
            _appSettings = appSettings.Value;
            _log = log;
        }

        public UserReponse Authenticate(User model)
        {
            var user = _users.SingleOrDefault(x => x.UserId == model.UserId && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);
            return new UserReponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(string userId)
        {
            var user = _users.SingleOrDefault(x => x.UserId == userId);
            return user;
        }
        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            
            return tokenHandler.WriteToken(token);
        }
    }
}
