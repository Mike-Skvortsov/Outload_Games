using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
	public class Feed
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Url { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime LastUpdate { get; set; }
		public bool Activity { get; set; } = true;
		public ICollection<News> News { get; set; }

	}

}
