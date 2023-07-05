using System.ComponentModel.DataAnnotations;

namespace CXPApp.Models
{
    public class Provider
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PersonType { get; set; }
        [Required]
        public string PersonalId { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public double Balance { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
