using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvinashBackEndAPI.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public string AccountType { get; set; }

        public Boolean Status { get; set; }

        public List<Transaction> DebitTransactions { get; set; }

        public List<Transaction> CreditTransactions { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public string TransferID { get; set; }

        public string TypeOfPayment { get; set; }

        public decimal Amount { get; set; }

        public DateTime TimeOfTransfer { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public int DebitAccountId { get; set; }
        public Account DebitAccount { get; set; }

        public int CreditAccountId { get; set; }
        public Account CreditAccount { get; set; }
    }
}
