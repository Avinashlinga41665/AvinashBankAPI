using System.ComponentModel.DataAnnotations;

namespace AvinashBackEndAPI.Models
{
    public class AccountTable
    {
        [Key]
        public int tmsID { get; set; }
        public string fromAccountName { get; set; }
        public int Debit { get; set; }
        public string toAccountName { get; set; }
        public int Credit { get; set; }

        public string transferType { get; set; }

        public bool status { get; set; }
    }
}
