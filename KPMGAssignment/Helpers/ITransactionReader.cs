using KPMGAssignment.Models.ViewModels;
using System.Threading.Tasks;

namespace KPMGAssignment.Helpers
{
    public interface ITransactionReader
    {
        bool ReadTransaction(TransactionViewModel row);
        Task<bool> ReadTransactionAsync(TransactionViewModel row);
    }
}