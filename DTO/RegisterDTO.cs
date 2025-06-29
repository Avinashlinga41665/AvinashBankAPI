namespace AvinashBackEndAPI.DTO
{
    public class RegisterDTO
    {

        public string AadharNumber { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }


        public string? FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}

