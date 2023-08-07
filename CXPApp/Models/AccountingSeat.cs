using System.ComponentModel.DataAnnotations;

namespace CXPApp.Models
{
    public class AccountingSeat
    {
        public int Id { get; set; }
        [Required]
        public int SeatId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string IdInventoryType { get; set; }
        [Required]
        public string AccountingAccount { get; set; }
        [Required]
        public string MovementType { get; set; }
        [Required]
        public DateTime SeatDate { get; set; }
        [Required]
        public double SeatAmount { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
