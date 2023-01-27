using AutoMapper;
using Entities;
using Outloud_Games.Model;

namespace Outloud_Games.MappingProfiles
{
	public class PresentationLayerMappingProfile : Profile
	{
		public PresentationLayerMappingProfile()
		{
			this.CreateMap<NewsModel, News>();
			this.CreateMap<News, NewsModel>();
			this.CreateMap<FeedModel, Feed>();
			this.CreateMap<Feed, FeedModel>();
			this.CreateMap<UserModel, User>();
			this.CreateMap<User, UserModel>();
		}
	}
}
