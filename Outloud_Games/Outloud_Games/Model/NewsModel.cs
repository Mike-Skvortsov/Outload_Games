using Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Outloud_Games.Model
{
	public class NewsModel
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Link { get; set; }
		public DateTime PublishDate { get; set; }
		public bool IsRead { get; set; }
		public int FeedId { get; set; }
	}
}
