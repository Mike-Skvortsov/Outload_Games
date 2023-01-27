using System;

namespace Outloud_Games.Model
{
	public class FeedModel
	{
		public string Url { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime LastUpdate { get; set; }
		public bool Activity { get; set; } = true;
	}
}
