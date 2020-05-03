using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Services
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenBuilder(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public string BuildToken(Users users)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder-key-that-is-long-enough-for-sha256"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim("id", users.id.ToString()),
                new Claim("username", users.username),
                new Claim("ShopsId", users.ShopsId.ToString()),
                new Claim("PositionsId", users.PositionsId.ToString())
            };
            var jwt = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: signingCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        public bool isExpiredToken()
        {
            //var user = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<Users>("user");
            string remember_token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            Console.WriteLine(remember_token);
            var token = new JwtSecurityTokenHandler().ReadJwtToken(remember_token);
            var username = token.Claims.First(claim => claim.Type == "username").Value;
            var ShopsId = int.Parse(token.Claims.First(claim => claim.Type == "ShopsId").Value);
            if (_context.Users.Any(u => u.username == username && u.ShopsId == ShopsId && u.remember_token == remember_token))
            {
                var jwttoken = new JwtSecurityTokenHandler().ReadToken(remember_token);
                var expTime = jwttoken.ValidTo;
                var currentTime = DateTime.UtcNow;
                //Console.WriteLine("remember_token " + remember_token);
                Console.WriteLine("jwttoken " + jwttoken);
                Console.WriteLine("exp " + expTime);
                Console.WriteLine("current " + currentTime);

                //var jwttoken2 = new JwtSecurityTokenHandler().ReadJwtToken(remember_token);
                //var jwttoken2 = jwttoken as JwtSecurityToken;
                //Console.WriteLine("ShopsId " + jwttoken2.Claims.First(claim => claim.Type == "ShopsId").Value);

                if (expTime >= currentTime)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
