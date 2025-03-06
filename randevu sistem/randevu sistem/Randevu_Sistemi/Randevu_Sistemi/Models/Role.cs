using System.ComponentModel.DataAnnotations;

namespace Randevu_Sistemi.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
