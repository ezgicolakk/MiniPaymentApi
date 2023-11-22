using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.API.Modals;
using PaymentApi.Business.Abstract;
using PaymentApi.Business.Modals;
using PaymentApi.DataAccess.Repositories.Modals;

namespace PaymentApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("pay")]
        public ActionResult Pay([FromBody] PayRequest request)
        {
            var result = _transactionService.Pay(request);
            return Ok(result);
        }

        [HttpPost("cancel")]
        public ActionResult Cancel([FromBody] CancelRequest request)
        {
            var result = _transactionService.Cancel(request);
            return Ok(result);
        }

        [HttpPost("refund")]
        public ActionResult Refund([FromBody] RefundRequest request)
        {
            var result = _transactionService.Refund(request);
            return Ok(result);
        }

        [HttpGet("report")]
        public ActionResult<TransactionReport> GetReport([FromQuery] ReportRequest request)
        {
            var report = _transactionService.GenerateReport(request);
            return Ok(report);
        }
    }
}
