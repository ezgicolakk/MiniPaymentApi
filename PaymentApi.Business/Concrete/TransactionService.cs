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
using PaymentApi.DataAccess.Repositories.Modals;

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
            using var dbTransaction = _dbContext.Database.BeginTransaction();

            try
            {
                var transaction = _dbContext.Transactions
           .Include(t => t.TransactionDetails)
           .FirstOrDefault(t => t.Id == new Guid(request.TransactionId));

                if (transaction == null)
                {
                    return new TransactionResult
                    {
                        Success = false,
                        Message = "Transaction not found."
                    };
                }

                if (transaction.Status == TransactionStatus.Success &&
                    transaction.TransactionDate.Date == DateTime.UtcNow.Date)
                {
                    var cancelId = Guid.NewGuid();

                    var cancelTransaction = new Transaction
                    {
                        Id = cancelId,
                        BankId = transaction.BankId,
                        TotalAmount = -transaction.TotalAmount,
                        NetAmount = 0,
                        Status = TransactionStatus.Success,
                        OrderReference = transaction.OrderReference,
                        TransactionDate = DateTime.UtcNow
                    };


                    var cancelTransactionDetail = new TransactionDetail
                    {
                        Id = Guid.NewGuid(),
                        TransactionId = cancelId,
                        TransactionType = TransactionType.Cancel,
                        Status = TransactionStatus.Success,
                        Amount = transaction.TotalAmount
                    };

                    _dbContext.TransactionDetails.Add(cancelTransactionDetail);

                    transaction.NetAmount = 0;

                    _dbContext.Transactions.Update(transaction);
                    _dbContext.Transactions.Add(cancelTransaction);
                    var result = _dbContext.SaveChanges();

                    if (result == 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        dbTransaction.Commit();
                    }

                    return new TransactionResult
                    {
                        Success = true,
                        Status = cancelTransaction.Status,
                        Message = "Cancellation successful.",
                        TransactionId = cancelId
                    };
                }
                else
                {
                    dbTransaction.Rollback();

                    return new TransactionResult
                    {
                        Success = false,
                        Message = "Cancellation not allowed for this transaction."
                    };
                }

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();

                return new TransactionResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }

        public TransactionReport GenerateReport(ReportRequest request)
        {
            var query = _dbContext.Transactions
                .Include(t => t.TransactionDetails)
                .Where(t =>
                    (request.BankId == null || t.BankId == request.BankId) &&
                    (request.Status == null || t.Status == request.Status) &&
                    (string.IsNullOrEmpty(request.OrderReference) || t.OrderReference == request.OrderReference) &&
                    (request.StartDate == null || t.TransactionDate >= request.StartDate) &&
                    (request.EndDate == null || t.TransactionDate <= request.EndDate));


            if (request.TransactionType != null)
            {
                query = query.Where(t => t.TransactionDetails.Any(td => td.TransactionType == request.TransactionType));
            }


            var report = new TransactionReport
            {
                Transactions = query.ToList(),
                GeneratedAt = DateTime.UtcNow
            };

            return report;
        }

        public TransactionResult Pay(PayRequest request)
        {
            try
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
                    Status = transaction.Status,
                    Success = true
                };

            }
            catch (Exception ex)
            {
                return new TransactionResult
                {
                    Success = false,
                    Message = ex.Message
                };

            }

        }

        public TransactionResult Refund(RefundRequest request)
        {
            using var dbTransaction = _dbContext.Database.BeginTransaction();

            try
            {
                var transaction = _dbContext.Transactions
             .Include(t => t.TransactionDetails)
             .FirstOrDefault(t => t.Id == new Guid(request.TransactionId));

                if (transaction == null)
                {
                    return new TransactionResult
                    {
                        Success = false,
                        Message = "Transaction not found."
                    };
                }

                if (transaction.Status == TransactionStatus.Success &&
                    (DateTime.UtcNow - transaction.TransactionDate).Days >= 1)
                {
                    var refundId = Guid.NewGuid();
                    var refundTransaction = new Transaction
                    {
                        Id = refundId,
                        BankId = transaction.BankId,
                        TotalAmount = -request.RefundAmount,
                        NetAmount = transaction.NetAmount - request.RefundAmount,
                        Status = TransactionStatus.Success,
                        OrderReference = transaction.OrderReference,
                        TransactionDate = DateTime.UtcNow
                    };

                    var refundTransactionDetail = new TransactionDetail
                    {
                        Id = Guid.NewGuid(),
                        TransactionId = refundId,
                        TransactionType = TransactionType.Refund,
                        Status = TransactionStatus.Success,
                        Amount = request.RefundAmount,
                    };

                    _dbContext.TransactionDetails.Add(refundTransactionDetail);


                    transaction.NetAmount = transaction.NetAmount - request.RefundAmount;

                    _dbContext.Transactions.Update(transaction);
                   
                    _dbContext.Transactions.Add(refundTransaction);

                    var result = _dbContext.SaveChanges();

                    if (result == 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        dbTransaction.Commit();
                    }

                    return new TransactionResult
                    {
                        Success = true,
                        Status = refundTransaction.Status,
                        Message = "Refund successful.",
                        TransactionId = refundId
                    };
                }
                else
                {
                    dbTransaction.Rollback();

                    return new TransactionResult
                    {
                        Success = false,                       
                        Message = "Refund not allowed for this transaction."                       
                    };
                }
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();

                return new TransactionResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}