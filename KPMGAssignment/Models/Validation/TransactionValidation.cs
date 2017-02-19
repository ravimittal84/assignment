using KPMGAssignment.Helpers;
using KPMGAssignment.Models.ViewModels;
using System.Collections.Generic;

namespace KPMGAssignment.Models.Validation
{
    public class TransactionValidation
    {
        public static void Validate(TransactionViewModel transaction)
        {
            List<string> errors = new List<string>();
            var result = true;
            if (string.IsNullOrEmpty(transaction.Account))
            {
                errors.Add("Account is required");
                result = false;
            }

            if (string.IsNullOrEmpty(transaction.Description))
            {
                errors.Add("Description is required");
                result = false;
            }

            if (!CurrencyHelper.ISOCurrencySymbols.Contains(transaction.CurrencyCode.ToUpper()))
            {
                errors.Add(string.Format($"{transaction.CurrencyCode} is not a valid currency code."));
                result = false;
            }

            decimal amount;
            if (!decimal.TryParse(transaction.Amount, out amount))
            {
                errors.Add(string.Format($"{transaction.Amount} is not a valid number."));
                result = false;
            }

            transaction.ValidationMessage = !result ? string.Join(", ", errors) : string.Empty;
            transaction.IsValid = result;
        }
    }
}
