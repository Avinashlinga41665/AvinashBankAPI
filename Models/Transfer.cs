using System.ComponentModel.DataAnnotations;

namespace AvinashBackEndAPI.Models
{
    public class Transfer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TransferID { get; set; }

        [Required]
        public string fromAccount { get; set; }

        [Required]
        public string toAccount { get; set; }

        public decimal Amount { get; set; }

        public DateTime TimeOfTransfer { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public int UserID { get; set; } 
        public User User { get; set; }

    }
}
