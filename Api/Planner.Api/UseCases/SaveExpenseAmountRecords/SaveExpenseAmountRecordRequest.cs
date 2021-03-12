using Planner.Api.Model;
using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.SaveExpenseAmountRecords
{
    public class SaveExpenseAmountRecordRequest
    {
        [Required]
        public System.Guid AccountId { get; set; }

        [Required]
        public System.Guid ExpenseId { get; set; }

        [Required]
        public AmountRecordModel[] AmountRecords { get; set; }
    }
}
