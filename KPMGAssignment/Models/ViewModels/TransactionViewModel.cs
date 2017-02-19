namespace KPMGAssignment.Models.ViewModels
{
    public class TransactionViewModel : ITransactionResult
    {
        public TransactionViewModel() { }

        public int ID { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public string Amount { get; set; }

        public string ValidationMessage { get; set; }

        public bool IsValid { get; set; }

    }
}
