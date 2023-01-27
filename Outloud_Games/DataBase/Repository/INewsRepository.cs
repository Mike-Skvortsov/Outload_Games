using Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Repository
{
	public interface INewsRepository
	{
		Task AddNewsAsync(News news);
		Task<ICollection<News>> GetUnreadOnDataAsync(DateTime date);
		Task MarkNewsAsReadAsync(int newsId);
	}
}
