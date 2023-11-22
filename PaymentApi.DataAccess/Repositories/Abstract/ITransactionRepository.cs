using Microsoft.EntityFrameworkCore.Update.Internal;
using PaymentApi.Core.Entities;
using PaymentApi.DataAccess.Repositories.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PaymentApi.DataAccess.Repositories.Abstract
{
    public interface ITransactionRepository
    {
        void SaveTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        Transaction GetTransaction(Guid transactionId);
        List<Transaction> GetTransactions(ReportRequest request);
    }
}
