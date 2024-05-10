using System.ComponentModel.DataAnnotations;

namespace Api_7._0.Models
{
    public class Doctor
    {
        [Key]
        public int IDDoctor { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Especialidad { get; set; }
        [Required]
        public string Hospital { get; set; }
    }
}
