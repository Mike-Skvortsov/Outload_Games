using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
	public class News
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public string Description { get; set; }
		public string Link { get; set; }
		public DateTime PublishDate { get; set; }
		[Required]
		public bool IsRead { get; set; }

		[ForeignKey("Feed")]
		public int FeedId { get; set; }
		public Feed Feed { get; set; }
	}
}
