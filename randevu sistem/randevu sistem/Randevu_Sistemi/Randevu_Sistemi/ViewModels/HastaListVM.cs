using System.ComponentModel.DataAnnotations;

namespace Randevu_Sistemi.ViewModels
{
	public class HastaListVM
	{
		public int Id { get; set; }
		public string HastaAd {  get; set; }
		
		public string HekimName { get; set; }
		public string HastalikName { get; set; }

	}
}
