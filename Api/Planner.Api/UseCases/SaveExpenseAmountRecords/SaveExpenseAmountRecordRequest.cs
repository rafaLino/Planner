using Planner.Api.Model;
using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.SaveExpenseAmountRecords
{
    public class SaveExpenseAmountRecordRequest
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string ExpenseId { get; set; }

        [Required]
        public AmountRecordModel[] AmountRecords { get; set; }
    }
}
