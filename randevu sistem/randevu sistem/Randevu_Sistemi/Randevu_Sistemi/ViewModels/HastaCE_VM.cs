namespace Randevu_Sistemi.ViewModels
{
	public class HastaCE_VM
	{
		public int Id { get; set; }
		public string HastaAd {  get; set; }
		public int HekimId { get; set; }
		public int HastalikTuruId { get; set; }
		public List<SelectItem> HekimList { get; set; }
		public List<SelectItem> HastalikTuruList { get; set; }
	}
}
