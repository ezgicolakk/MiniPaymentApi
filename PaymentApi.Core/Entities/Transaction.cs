using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PaymentApi.Core.Entities
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public int BankId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
        public TransactionStatus Status { get; set; }
        public string OrderReference { get; set; }
        public DateTime TransactionDate { get; set; }
        public List<TransactionDetail> TransactionDetails { get; set; }

    }
}
