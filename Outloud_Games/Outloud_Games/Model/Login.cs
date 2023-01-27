using System.ComponentModel.DataAnnotations;

namespace Outloud_Games.Model
{
	public class Login
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
