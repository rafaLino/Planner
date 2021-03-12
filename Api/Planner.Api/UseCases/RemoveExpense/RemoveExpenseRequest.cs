using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.RemoveExpense
{
    public class RemoveExpenseRequest
    {
        [Required]
        public System.Guid AccountId { get; set; }

        [Required]
        public System.Guid ExpenseId { get; set; }
    }
}
