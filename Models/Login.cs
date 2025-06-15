using System.ComponentModel.DataAnnotations;

namespace AvinashBackEndAPI.Models
{
    public partial class Login
    {
        [Key]

        public int Id { get; set; }
        [Required]

        public string Userloginname { get; set; }
        [Required]

        public string Userloginpwd { get; set; }


    }
}
