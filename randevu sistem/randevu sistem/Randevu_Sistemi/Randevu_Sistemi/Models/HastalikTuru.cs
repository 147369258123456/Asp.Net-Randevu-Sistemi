using System.ComponentModel.DataAnnotations;

namespace Randevu_Sistemi.Models
{
	public class HastalikTuru
	{
		[Key] //Primary key
		public int Id { get; set; }
		
		public string HastalikAdi { get; set; }
	}
}
