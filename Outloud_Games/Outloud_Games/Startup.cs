using Autofac;
using Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Outloud_Games.MappingProfiles;
using Newtonsoft.Json;
using Auth;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Outloud_Games
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAutoMapper(x => x.AddProfile(new PresentationLayerMappingProfile()));
			services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DB")));
			services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Outloud_Games", Version = "v1" });
			});
			var authOptionsConfiguration = Configuration.GetSection("Auth").Get<AuthOptions>();
			services.AddOptions<AuthOptions>().Bind(Configuration.GetSection("Auth"));

			if (authOptionsConfiguration == null || string.IsNullOrEmpty(authOptionsConfiguration.Secret))
			{
				authOptionsConfiguration.Secret = Environment.GetEnvironmentVariable("AUTH_SECRET") ?? "defaultValue";
			}


			services.AddSingleton(authOptionsConfiguration);
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{

			options.RequireHttpsMetadata = false;
			options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = authOptionsConfiguration.Issuer,

				ValidateAudience = true,
				ValidAudience = authOptionsConfiguration.Audience,
				ValidateLifetime = true,

				IssuerSigningKey = authOptionsConfiguration.GetSynnetricSecurityKey(),
				ValidateIssuerSigningKey = true,
			};
		});
		}
		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.RegisterModule(new DataAccessRegistrationModule());
		}
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
			using (var context = new DatabaseContext(optionsBuilder.Options))
			{
				context.Database.Migrate();
			}

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Outloud_Games v1"));
			}
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
