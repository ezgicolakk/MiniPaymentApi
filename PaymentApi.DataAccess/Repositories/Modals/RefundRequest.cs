namespace PaymentApi.API.Modals
{
    public class RefundRequest
    {
        public int TransactionId { get; set; }
        public decimal RefundAmount { get; set; }
    }
}
