namespace PaymentApi.API.Modals
{
    public class RefundRequest
    {
        public string TransactionId { get; set; }
        public decimal RefundAmount { get; set; }
    }
}
