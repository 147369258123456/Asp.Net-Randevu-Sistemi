using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Randevu_Sistemi.Models
{
    public class Kullanici
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        public Role UserRole { get; set; }
    }
}
