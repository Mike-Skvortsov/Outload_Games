using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Database.Repository
{
	public class FeedRepository : IFeedRepository
	{
		private readonly DatabaseContext _context;
		public FeedRepository(DatabaseContext context)
		{
			this._context = context;
		}
		public async Task<ICollection<Feed>> GetAllActivityRSSAsync()
		{
			return await this._context.Feeds.Include(x => x.News).Where(x => x.Activity == true).ToListAsync();
		}
		public async Task AddRSSAsync(Feed feed)
		{
			await _context.Feeds.AddAsync(feed);
			await _context.SaveChangesAsync();
		}

		private SyndicationFeed GetSyndicationFeed(string feedUrl)
		{
			using (var reader = XmlReader.Create(feedUrl))
			{
				return SyndicationFeed.Load(reader);
			}
		}
	}
}
