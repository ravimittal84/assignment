namespace KPMGAssignment.Models
{
    public class Transaction : ITransaction
    {
        public int ID { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
    }
}
