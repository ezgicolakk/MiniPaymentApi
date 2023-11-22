using PaymentApi.Business.Abstract;
using PaymentApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApi.Business.Concrete
{
    public class YapiKredi : IBank
    {
        public Transaction ProcessCancel(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Transaction ProcessPay(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Transaction ProcessRefund(Transaction transaction, decimal refundAmount)
        {
            throw new NotImplementedException();
        }
    }
}
