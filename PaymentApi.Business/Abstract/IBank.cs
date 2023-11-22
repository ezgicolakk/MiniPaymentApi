using PaymentApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApi.Business.Abstract
{
    public interface IBank
    {
        Transaction ProcessPay(Transaction transaction);
        Transaction ProcessCancel(Transaction transaction);
        Transaction ProcessRefund(Transaction transaction, decimal refundAmount);
    }
}
