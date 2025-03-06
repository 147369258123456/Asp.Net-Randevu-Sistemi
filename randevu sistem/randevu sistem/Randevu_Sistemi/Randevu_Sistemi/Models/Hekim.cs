using System.ComponentModel.DataAnnotations;

namespace Randevu_Sistemi.Models
{
    public class Hekim
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string HekimAd {  get; set; }
        
        
       

    }
}
