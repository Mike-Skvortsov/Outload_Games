using AutoMapper;
using Database.Repository;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Outloud_Games.Model;
using System;
using System.Threading.Tasks;

namespace Outloud_Games.Controllers
{
	[Authorize]
	[Route("news")]
	[ApiController]
	public class NewsController : ControllerBase
	{
		private readonly INewsRepository _newsRepository;
		private readonly IMapper _mapper;
		public NewsController(INewsRepository newsRepository, IMapper mapper)
		{
			_newsRepository = newsRepository;
			_mapper = mapper;
		}
		[HttpGet("unread")]
		public async Task<IActionResult> GetUnreadNews(DateTime date)
		{
			var news = await _newsRepository.GetUnreadOnDataAsync(date);
			return this.Ok(news);
		}

		[HttpPost("create")]
		public async Task<IActionResult> Create([FromBody] NewsModel model)
		{
			var news = this._mapper.Map<News>(model);
			await _newsRepository.AddNewsAsync(news);
			return Ok();
		}
		[HttpPut("{id}/read")]
		public async Task<IActionResult> MarkAsRead([FromRoute] int id)
		{
			await _newsRepository.MarkNewsAsReadAsync(id);
			return Ok();
		}
	}
}
