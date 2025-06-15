using System.ComponentModel.DataAnnotations;

namespace AvinashBackEndAPI.Models
{
    public class Register
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AadharNumber { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string MobileNumber { get; set; }

    }
}
