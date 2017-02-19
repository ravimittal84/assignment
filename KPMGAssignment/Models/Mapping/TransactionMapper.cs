using KPMGAssignment.Models.Validation;
using KPMGAssignment.Models.ViewModels;

namespace KPMGAssignment.Models.Mapping
{
    public static class TransactionMapper
    {
        public static Transaction ToTransaction(this TransactionViewModel model)
        {
            TransactionValidation.Validate(model);
            return new Transaction
            {
                Account = model.Account,
                Amount = model.IsValid ? decimal.Parse(model.Amount) : 0,
                CurrencyCode = model.CurrencyCode,
                Description = model.Description,
                ID = model.ID
            };
        }

        public static TransactionViewModel ToViewModel(this Transaction model)
        {
            return new TransactionViewModel
            {
                Account = model.Account,
                Amount = model.Amount.ToString(),
                CurrencyCode = model.CurrencyCode,
                Description = model.Description,
                ID = model.ID
            };
        }
    }
}
