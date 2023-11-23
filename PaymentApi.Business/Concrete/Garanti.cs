using PaymentApi.Business.Abstract;
using PaymentApi.Core.Entities;
using PaymentApi.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApi.Business.Concrete
{
    public class Garanti : IBank
    {
        public Transaction ProcessCancel(Transaction transaction)
        {
            transaction.Status = TransactionStatus.Success;

            return transaction;
        }

        public Transaction ProcessPay(Transaction transaction)
        {
            transaction.Status = TransactionStatus.Success;

            return transaction;
        }

        public Transaction ProcessRefund(Transaction transaction, decimal refundAmount)
        {
            transaction.Status = TransactionStatus.Success;

            return transaction;
        }
    }
}
