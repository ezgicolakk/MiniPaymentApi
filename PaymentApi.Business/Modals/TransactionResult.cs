using PaymentApi.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApi.Business.Modals
{
    public class TransactionResult
    {
        public bool Success { get; set; }
        public Guid TransactionId { get; internal set; }
        public TransactionStatus Status { get; internal set; }
        public string Message { get; internal set; }
    }
}
