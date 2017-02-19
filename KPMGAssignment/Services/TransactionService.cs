using KPMGAssignment.Helpers;
using KPMGAssignment.Models;
using KPMGAssignment.Models.Mapping;
using KPMGAssignment.Models.Validation;
using KPMGAssignment.Models.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KPMGAssignment.Services
{
    public class TransactionService : ITransactionService
    {
        private IRepository _repository;

        public TransactionService()
        {
            _repository = new Repository();
        }

        public TransactionService(IRepository repo)
        {
            _repository = repo;
        }


        public async Task<bool> DeleteTransaction(int id)
        {
            _repository.Delete<Transaction>(id);
            await _repository.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<TransactionViewModel>> GetAllTransactions()
        {
            var results = await _repository.GetList<Transaction>().ToListAsync();
            return results.Select(t => t.ToViewModel());
        }


        public async Task<TransactionViewModel> GetTransactionByID(int id)
        {
            var tran = await _repository.GetAsync<Transaction>(id);
            return tran != null ? tran.ToViewModel() : new TransactionViewModel();
        }


        public async Task<ImportResultViewModel> ProcessFile(Stream stream)
        {
            var result = new ImportResultViewModel
            {
                LinesImported = 0,
                LinesIgnored = new List<string>()
            };

            var linesProcessed = 1;
            using (var reader = new CSVTransactionReader(stream))
            {
                var tran = new TransactionViewModel();
                while (await reader.ReadTransactionAsync(tran))
                {
                    TransactionValidation.Validate(tran);
                    if (tran.IsValid)
                    {
                        result.LinesImported++;
                        _repository.Add(tran.ToTransaction());
                    }
                    else
                    {
                        result.LinesIgnored.Add($"{linesProcessed} ignored, Reason: {tran.ValidationMessage}.");
                    }
                    linesProcessed++;
                }

                await _repository.SaveChangesAsync();
                reader.Close();
            }

            return result;
        }


        public async Task<bool> SaveTransaction(TransactionViewModel transaction)
        {
            var tran = await _repository.GetAsync<Transaction>(transaction.ID);
            if (tran != null)
            {
                _repository.Attach(transaction.ToTransaction());
            }
            else
            {
                _repository.Add(transaction);
            }

            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
