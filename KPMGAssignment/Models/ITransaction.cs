namespace KPMGAssignment.Models
{
    public interface ITransaction
    {
        string Account { get; set; }
        //decimal Amount { get; set; }
        string CurrencyCode { get; set; }
        string Description { get; set; }
        int ID { get; set; }
    }

    public interface ITransactionResult : ITransaction
    {
        string ValidationMessage { get; set; }
    }
}