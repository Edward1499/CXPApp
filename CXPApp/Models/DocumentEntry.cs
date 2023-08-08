using System.ComponentModel.DataAnnotations;

namespace CXPApp.Models
{
    public class DocumentEntry
    {
        public int Id { set; get; }
        [Required]
        public string DocumentNumber { set; get; }
        [Required]
        public string InvoiceNumber { set; get; }
        [Required]
        public DateTime DocumentDay { set; get; }
        [Required]
        public double amount { set; get; }
        [Required]
        public DateTime RegistrationDate { set; get; }
        [Required]
        public string Provider { set; get; }
        [Required]
        public bool IsActive { get; set; }
    }
}
