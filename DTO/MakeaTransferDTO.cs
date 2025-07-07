using AvinashBackEndAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace AvinashBackEndAPI.DTO
{
    public class MakeaTransferDTO
    {
        public string FromAccountNumber { get; set; }
        public string ToAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string TransferType { get; set; }
    }


}
