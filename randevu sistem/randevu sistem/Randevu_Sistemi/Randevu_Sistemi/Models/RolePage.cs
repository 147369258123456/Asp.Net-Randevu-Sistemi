using System.ComponentModel.DataAnnotations;

namespace Randevu_Sistemi.Models
{
    public class RolePage
    {
        [Key]
        public int Id { get; set; }
        public Role PageRole { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}
