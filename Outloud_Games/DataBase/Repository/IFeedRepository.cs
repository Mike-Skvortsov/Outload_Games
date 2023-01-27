using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
{
	public interface IFeedRepository
	{
		Task AddRSSAsync(Feed feed);
		Task<ICollection<Feed>> GetAllActivityRSSAsync();
	}
}
