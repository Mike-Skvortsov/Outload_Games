using Auth;
using Database.Repository;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Outloud_Games.Model;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Threading.Tasks;

namespace Outloud_Games.Controllers
{
	public class AuthController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly IOptions<AuthOptions> authOptionsConfiguration;

		public AuthController(IUserRepository userRepository, IOptions<AuthOptions> authOptionsConfiguration)
		{
			this._userRepository = userRepository;
			this.authOptionsConfiguration = authOptionsConfiguration;
		}
		[Route("login")]
		[HttpPost]
		public async Task<IActionResult> Login([FromBody] Login request)
		{
			var result = await this._userRepository.Search(request.Email, request.Password);
			if (result != null)
			{
				var token = GenerateJWT(result);

				return Ok(new
				{
					accessToken = token
				});
			}
			return Unauthorized();
		}
		private string GenerateJWT(User user)
		{
			var authParams = authOptionsConfiguration.Value;

			var securityKey = authParams.GetSynnetricSecurityKey();
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

			var claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
			};

			var token = new JwtSecurityToken(authParams.Issuer,
				authParams.Audience,
				claims,
				expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
