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
            var jwt = new JwtSecurityToken(expires: DateTime.Now.AddMinutes(5), signingCredentials: signingCredentials);
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
    }
}
