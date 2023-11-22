using PaymentApi.API.Modals;
using PaymentApi.Business.Modals;
using PaymentApi.DataAccess.Repositories.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApi.Business.Abstract
{
    public interface ITransactionService
    {
        TransactionResult Pay(PayRequest request);
        TransactionResult Cancel(CancelRequest request);
        TransactionResult Refund(RefundRequest request);
        TransactionReport GenerateReport(ReportRequest request);
    }
}
