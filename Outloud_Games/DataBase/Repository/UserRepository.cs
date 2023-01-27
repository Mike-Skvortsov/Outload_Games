using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly DatabaseContext _context;
		public UserRepository(DatabaseContext context)
		{
			this._context = context;
		}
		public async Task AddUserAsync(User user)
		{
			await this._context.Users.AddAsync(user);
			await this._context.SaveChangesAsync();
		}
		public async Task<User> Search(string email, string passwordParam)
		{
			return await this._context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == passwordParam);
		}
	}
}
