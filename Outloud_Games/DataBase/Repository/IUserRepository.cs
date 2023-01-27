using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
{
	public interface IUserRepository
	{
		Task AddUserAsync(User user);
		Task<User> Search(string email, string passwordParam);
	}
}
