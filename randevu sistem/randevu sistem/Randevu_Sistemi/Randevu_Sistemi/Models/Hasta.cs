using System.ComponentModel.DataAnnotations;

namespace Randevu_Sistemi.Models
{
	public class Hasta
	{
		[Key]
		public int Id { get; set; }
		
		public string HastaAd {  get; set; }
	
		public Hekim hekim {  get; set; }
		
		public HastalikTuru hastalikTuru { get; set; }
		
		

	}
}
