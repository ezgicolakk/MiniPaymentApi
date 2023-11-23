using PaymentApi.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApi.DataAccess.Repositories.Modals
{
    public class ReportRequest
    {
        public int? BankId { get; set; }
        public TransactionStatus? Status { get; set; }
        public string? OrderReference { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TransactionType? TransactionType { get; set; } = null;
    }
}
