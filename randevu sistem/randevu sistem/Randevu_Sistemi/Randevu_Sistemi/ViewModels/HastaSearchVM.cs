namespace Randevu_Sistemi.ViewModels
{
	public class HastaSearchVM
	{
		public string HastaAd { get; set; } //arama kriteri

		
		public int HastalikTuruId { get; set; }
		public int HekimId { get; set; } 

		public List<SelectItem> HekimList { get; set; } //arama kriteri
		public List<SelectItem> HastalikTuruList { get; set; } //arama kriteri
		public List<HastaListVM> ResultList { get; set; } // bulunan hastalar
	}
}
