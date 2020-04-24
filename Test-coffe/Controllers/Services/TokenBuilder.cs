using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Test_coffe.Controllers.Services
{
    public class TokenBuilder : ITokenBuilder
    {
        public string BuildToken(string username)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder-key-that-is-long-enough-for-sha256"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            //var claims = new Claim[]
            //{
            //    new Claim(JwtRegisteredClaimNames.Sub, username)
            //};
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim("USERID", "1")
            };
<<<<<<< HEAD
            var jwt = new JwtSecurityToken(expires: DateTime.Now.AddMinutes(5), signingCredentials: signingCredentials);
=======
            var jwt = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(5), signingCredentials: signingCredentials);
>>>>>>> 1e5fa3f4d55602f90e120414cf434886acc18128
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;

            //var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder-key-that-is-long-enough-for-sha256"));
            //var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            //var claims = new Claim[]
            //{
            //    new Claim(JwtRegisteredClaimNames.Sub, username)
            //};

            //var jwt = new JwtSecurityToken("dan dan dan dan",
            //    "dan dan dan dan",
            //    claims: claims,
            //    expires: DateTime.Now.AddMinutes(120),
            //    signingCredentials: signingCredentials);
            //var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            //return encodedJwt;
        }
<<<<<<< HEAD
=======

        //public bool isExpiredToken(string remember_token)
        //{
        //    var jwttoken = new JwtSecurityTokenHandler().ReadToken(remember_token);
        //    var expTime = jwttoken.ValidTo;
        //    var currentTime = DateTime.UtcNow;
        //    Console.WriteLine("jwttoken " + jwttoken);
        //    Console.WriteLine("exp " + expTime);
        //    Console.WriteLine("current " + currentTime);

        //    if (expTime >= currentTime)
        //    {
        //        return false;
        //    }
        //    return false;
        //}

        public bool isExpiredToken()
        {
            var user = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<Users>("user");
            string remember_token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            if (_context.Users.Any(u => u.username == user.username && u.remember_token == remember_token))
            {
                var jwttoken = new JwtSecurityTokenHandler().ReadToken(remember_token);
                var expTime = jwttoken.ValidTo;
                var currentTime = DateTime.UtcNow;
                Console.WriteLine("remember_token " + remember_token);
                Console.WriteLine("jwttoken " + jwttoken);
                Console.WriteLine("exp " + expTime);
                Console.WriteLine("current " + currentTime);

                if (expTime >= currentTime)
                {
                    return false;
                }
            }
            return true;
        }
>>>>>>> 1e5fa3f4d55602f90e120414cf434886acc18128
    }
}
