using KPMGAssignment.Models.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KPMGAssignment.Services
{
    public interface ITransactionService
    {
        Task<ImportResultViewModel> ProcessFile(Stream stream);

        Task<bool> SaveTransaction(TransactionViewModel transaction);

        Task<bool> DeleteTransaction(int id);

        Task<IEnumerable<TransactionViewModel>> GetAllTransactions();

        Task<TransactionViewModel> GetTransactionByID(int id);
    }
}
