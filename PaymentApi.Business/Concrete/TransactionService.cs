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
using Microsoft.EntityFrameworkCore;
using PaymentApi.Core.Enums;

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
            var transaction = _dbContext.Transactions
           .Include(t => t.TransactionDetails)
           .FirstOrDefault(t => t.Id == new Guid(request.TransactionId.ToString()));

            if (transaction == null)
            {
                return new TransactionResult
                {
                    Success = false,
                    Message = "Transaction not found."
                };
            }

            if (transaction.Status == (System.Transactions.TransactionStatus)TransactionStatus.Success &&
                transaction.TransactionDate.Date == DateTime.UtcNow.Date)
            {
               
                var cancelTransaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    BankId = transaction.BankId,
                    TotalAmount = -transaction.TotalAmount,
                    NetAmount = -transaction.NetAmount,
                    Status = (System.Transactions.TransactionStatus)TransactionStatus.Success,
                    OrderReference = transaction.OrderReference,
                    TransactionDate = DateTime.UtcNow
                };

                
                var cancelTransactionDetail = new TransactionDetail
                {
                    Id = Guid.NewGuid(),
                    TransactionType = TransactionType.Cancel,
                    Status = TransactionStatus.Success,
                    Amount = -transaction.TotalAmount
                };

                cancelTransaction.TransactionDetails.Add(cancelTransactionDetail);

                transaction.NetAmount = 0;

             
                _dbContext.Transactions.Add(cancelTransaction);
                _dbContext.SaveChanges();

                return new TransactionResult
                {
                    Success = true,
                    Message = "Cancellation successful."
                };
            }
            else
            {
                return new TransactionResult
                {
                    Success = false,
                    Message = "Cancellation not allowed for this transaction."
                };
            }
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