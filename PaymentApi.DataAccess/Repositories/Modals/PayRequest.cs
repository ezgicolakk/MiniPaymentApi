namespace PaymentApi.API.Modals
{
    public class PayRequest
    {
        public int BankId { get; set; }
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string OrderReference { get; set; } = string.Empty;
        public string CVV { get; set; }
    }
}
