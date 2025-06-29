using System.ComponentModel.DataAnnotations;

namespace AvinashBackEndAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AadharNumber { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public string LoginID { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public List<ScheduleTransfer> ScheduleTransfers { get; set; }
    }

}
