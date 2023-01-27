using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Database.Repository
{
	public class NewsRepository : INewsRepository
	{
		private readonly DatabaseContext _context;
		public NewsRepository(DatabaseContext context)
		{
			this._context = context;
		}
		public async Task<ICollection<News>> GetUnreadOnDataAsync(DateTime date)
		{
			return await this._context.News.Include(x => x.Feed).Where(x => x.PublishDate.Day == date.Day && x.IsRead == false).ToListAsync();
		}
		public async Task AddNewsAsync(News news)
		{
			await this._context.News.AddAsync(news);
			await this._context.SaveChangesAsync();
		}
		public async Task MarkNewsAsReadAsync(int newsId)
		{
			var news = await _context.News.FindAsync(newsId);
			if (news != null)
			{
				news.IsRead = true;
				await _context.SaveChangesAsync();
			}
		}
	}
}
