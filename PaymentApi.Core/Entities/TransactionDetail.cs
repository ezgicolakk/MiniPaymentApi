using PaymentApi.Core.Enums;
using System.ComponentModel.DataAnnotations;


namespace PaymentApi.Core.Entities
{
    public class TransactionDetail
    {
        [Key]
        public Guid Id { get; set; }
        public Guid TransactionId { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionStatus Status { get; set; }
        public decimal Amount { get; set; }

    }
}
