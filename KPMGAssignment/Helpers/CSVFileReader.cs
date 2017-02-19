using KPMGAssignment.Models.ViewModels;
using System.IO;
using System.Threading.Tasks;

namespace KPMGAssignment.Helpers
{
    public class CSVTransactionReader : StreamReader, ITransactionReader
    {
        public CSVTransactionReader(Stream stream)
            : base(stream)
        {
        }

        public bool ReadTransaction(TransactionViewModel row)
        {
            var lineText = ReadLine();

            return GetTransaction(row, lineText);
        }

        public async Task<bool> ReadTransactionAsync(TransactionViewModel row)
        {
            var lineText = await ReadLineAsync();

            return GetTransaction(row, lineText);
        }

        private bool GetTransaction(TransactionViewModel row, string lineText)
        {
            if (string.IsNullOrWhiteSpace(lineText))
                return false;

            var str = lineText.Split(',');
            row.Account = str.Length > 0 ? str[0] : string.Empty;
            row.Description = str.Length > 1 ? str[1] : string.Empty;
            row.CurrencyCode = str.Length > 2 ? str[2] : string.Empty;
            row.Amount = str.Length > 3 ? str[3] : string.Empty;

            return true;
        }

    }
}
