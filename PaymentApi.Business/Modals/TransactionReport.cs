using PaymentApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApi.Business.Modals
{
    public class TransactionReport
    {
        public List<Transaction> Transactions { get; set;}
        public DateTime GeneratedAt { get; set; }
    }
}
