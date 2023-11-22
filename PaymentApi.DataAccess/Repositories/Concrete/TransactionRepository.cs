using Microsoft.EntityFrameworkCore;
using PaymentApi.DataAccess.Repositories.Abstract;
using PaymentApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApi.DataAccess.Context;
using PaymentApi.DataAccess.Repositories.Modals;

namespace PaymentApi.DataAccess.Repositories.Concrete
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PaymentDbContext _dbContext;

        public TransactionRepository(PaymentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Transaction GetTransaction(Guid transactionId)
        {
            return _dbContext.Transactions.FirstOrDefault(t => t.Id == transactionId);
        }

        public List<Transaction> GetTransactions(ReportRequest request)
        {
            var query = _dbContext.Transactions.AsQueryable();

            if (request.BankId != null)
            {
                query = query.Where(t => t.BankId == request.BankId);
            }

            if (request.Status != null)
            {
                query = query.Where(t => (Core.Enums.TransactionStatus)t.Status == request.Status.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.OrderReference))
            {
                query = query.Where(t => t.OrderReference == request.OrderReference);
            }

            if (request.StartDate != null && request.EndDate != null)
            {
                query = query.Where(t => t.TransactionDate >= request.StartDate && t.TransactionDate <= request.EndDate);
            }

            return query.ToList();
        }

        public void SaveTransaction(Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
        }

        public void UpdateTransaction(Transaction transaction)
        {
            _dbContext.Transactions.Update(transaction);
            _dbContext.SaveChanges();
        }
    }
}
