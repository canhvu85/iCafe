using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Test_coffe.Models;

namespace Test_coffe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyController : ControllerBase
    {
		private readonly ApplicationDbContext _context;

		public VerifyController(ApplicationDbContext context)
		{
			_context = context;
		}

		//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		//[Authorize(JwtBearerDefaults.AuthenticationScheme)]
		//[HttpPost("aa/{abc}")]
		//public IActionResult VerifyToken(string? abc)
  //      {
		//	//var user = HttpContext.Session.GetObjectFromJson<Users>("user");
		//	//Console.WriteLine(user.username);
		//	Console.WriteLine(abc);
		//	Console.WriteLine("dan");
		//	//if (_context.Users.Any(u => u.username == "dan" && u.token == token))
		//	//{
		//	//	Console.WriteLine("OK");
		//	//	Console.WriteLine(token);
		//	//	return NoContent();
		//	//}

		//	return BadRequest();
		//}

		[HttpPost]
		public async Task<ActionResult<Users>> VerifyToken(Users users)
		{
			//var user = HttpContext.Session.GetObjectFromJson<Users>("user");
			Console.WriteLine("users.token  " + users.token);

			return NoContent();
		}

		//[HttpPost]
		//public bool ValidateCurrentToken(string token)
		//{

		//	var myIssuer = "dan dan dan dan";
		//	var myAudience = "dan dan dan dan";

		//	var tokenHandler = new JwtSecurityTokenHandler();
		//	var tokenDescriptor = new SecurityTokenDescriptor
		//	{
		//		Subject = new ClaimsIdentity(new Claim[]
		//		{
		//	new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
		//		}),
		//		Expires = DateTime.UtcNow.AddDays(7),
		//		Issuer = myIssuer,
		//		Audience = myAudience,
		//		SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
		//	};

		//	var token = tokenHandler.CreateToken(tokenDescriptor);
		//	return tokenHandler.WriteToken(token);
		//}
	}
}