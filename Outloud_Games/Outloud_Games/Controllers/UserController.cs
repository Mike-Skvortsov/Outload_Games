using AutoMapper;
using Database.Repository;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Outloud_Games.Model;
using System.Threading.Tasks;

namespace Outloud_Games.Controllers
{
	[Route("user")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		public UserController(IUserRepository userRepository, IMapper mapper)
		{
			this._userRepository = userRepository;
			this._mapper = mapper;
		}
		[HttpPost("create")]
		public async Task<IActionResult> Create([FromBody] UserModel model)
		{
			var user = this._mapper.Map<User>(model);
			await _userRepository.AddUserAsync(user);
			return this.Ok();
		}
	}
}
