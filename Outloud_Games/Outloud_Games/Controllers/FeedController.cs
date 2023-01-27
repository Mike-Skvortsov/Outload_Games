using AutoMapper;
using Database.Repository;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Outloud_Games.Model;
using System.Threading.Tasks;

namespace Outloud_Games.Controllers
{
	[Authorize]
	[ApiController]
	[Route("feed")]
	public class FeedController : ControllerBase
	{
		private readonly IFeedRepository _feedRepository;
		private readonly IMapper _mapper;
		public FeedController(IFeedRepository feedRepository, IMapper mapper)
		{
			_feedRepository = feedRepository;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllActivityAsync()
		{
			var feeds = await this._feedRepository.GetAllActivityRSSAsync();
			return this.Ok(feeds);
		}	
		[HttpPost("create")]
		public async Task<IActionResult> AddFeed([FromBody] FeedModel url)
		{
			var feed = this._mapper.Map<Feed>(url);
			if (feed == null)
			{
				return BadRequest();
			}

			await _feedRepository.AddRSSAsync(feed);
			return this.Ok();
		}

	}
}
