using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CenterFieldCoffee.Models
{
    public class Store
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string? Name { get; set; }
        [NotMapped]
        public string? opening_time { get; set; }
        [Required]
        public Int16 opening_hour { get; set; }
        [Required]
        public Int16 opening_minute { get; set; }
        [NotMapped]
        public string? closing_time { get; set; }
        [Required]
        public Int16 closing_hour { get; set; }
        [Required]
        public Int16 closing_minute { get; set; }
        [NotMapped]
        public bool is_open {  get; set; }
        public DateTime create_date { get; set; } = DateTime.Now;
        public DateTime? update_date { get; set; }
    }
}
