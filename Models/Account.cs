using System.ComponentModel.DataAnnotations;

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

        public string Status { get; set; }

        // 1 Account → Many Debit Transactions
        public List<Transaction> DebitTransactions { get; set; }

        // 1 Account → Many Credit Transactions
        public List<Transaction> CreditTransactions { get; set; }
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

        // FK for Debit Account
        public int DebitAccountId { get; set; }
        public Account DebitAccount { get; set; }

        // FK for Credit Account
        public int CreditAccountId { get; set; }
        public Account CreditAccount { get; set; }
    }
}
