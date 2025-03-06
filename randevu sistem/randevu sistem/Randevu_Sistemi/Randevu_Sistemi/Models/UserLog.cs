using System.ComponentModel.DataAnnotations;

namespace Randevu_Sistemi.Models
{
	public class UserLog
	{
		[Key]
		public int Id { get; set; }

		public int UserId { get; set; }

		public string Controller { get; set; }

		public string Action { get; set; }

		public DateTime LogTime { get; set; }

		public string IpAdress { get; set; }
	}
}
