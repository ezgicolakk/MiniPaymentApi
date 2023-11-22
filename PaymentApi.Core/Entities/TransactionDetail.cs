using PaymentApi.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TransactionStatus = PaymentApi.Core.Enums.TransactionStatus;

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
