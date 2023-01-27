using Autofac;
using Database.Repository;

namespace Database
{
	public class DataAccessRegistrationModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<DatabaseContext>().AsSelf().InstancePerLifetimeScope();
			builder.RegisterType<NewsRepository>().As<INewsRepository>();
			builder.RegisterType<FeedRepository>().As<IFeedRepository>();
			builder.RegisterType<UserRepository>().As<IUserRepository>();
		}
	}
}
