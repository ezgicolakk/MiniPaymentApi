using MailChimp.Net.Core;
using PaymentApi.API.Modals;
using PaymentApi.Business.Abstract;
using PaymentApi.Business.Modals;
using PaymentApi.DataAccess.Repositories.Abstract;
using PaymentApi.DataAccess.Repositories.Concrete;
using PaymentApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApi.DataAccess.Context;

namespace PaymentApi.Business.Concrete
{
    public class TransactionService : ITransactionService
    {
        private readonly PaymentDbContext _dbContext;

        public TransactionService(PaymentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public TransactionResult Cancel(CancelRequest request)
        {
            throw new NotImplementedException();
        }

        public TransactionReport GenerateReport(DataAccess.Repositories.Modals.ReportRequest request)
        {
            throw new NotImplementedException();
        }

        public TransactionResult Pay(PayRequest request)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                BankId = request.BankId,
                TotalAmount = request.Amount,
                NetAmount = request.Amount, 
                Status = 0, 
                OrderReference = request.OrderReference,
                TransactionDate = DateTime.Now,
                TransactionDetails = new List<TransactionDetail>
            {
                new TransactionDetail
                {
                    Id = Guid.NewGuid(),
                    TransactionType = 0,
                    Status = 0, 
                    Amount = request.Amount
                }
            }
            };

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return new TransactionResult
            {
                TransactionId = transaction.Id,
                Status = (Core.Enums.TransactionStatus)transaction.Status
            };
        }

        public TransactionResult Refund(RefundRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
