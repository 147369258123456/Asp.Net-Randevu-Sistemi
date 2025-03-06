using System.ComponentModel.DataAnnotations;

namespace Randevu_Sistemi.ViewModels
{
	public class LoginVM
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
