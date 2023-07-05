using System.ComponentModel.DataAnnotations;

namespace CXPApp.Models
{
    public class PaymentConcept
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
