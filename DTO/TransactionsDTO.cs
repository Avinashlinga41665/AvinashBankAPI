using AvinashBackEndAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace AvinashBackEndAPI.DTO
{
    public class TransactionsDTO
    {
        public int Id { get; set; }

        public string TransferID { get; set; }

        public string fromAccount { get; set; }
        public string toAccount { get; set; }

        public decimal Amount { get; set; }

        public DateTime TimeOfTransfer { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

    }
}
