using System.ComponentModel.DataAnnotations;

namespace AvinashBackEndAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AadharNumber { get; set; }
        [Required]


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public string LoginName { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        public List<Transfer> ScheduleTransfers { get; set; }
        public List<Account> Accounts { get; set; }

    }

}
