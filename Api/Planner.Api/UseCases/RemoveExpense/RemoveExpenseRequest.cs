using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.RemoveExpense
{
    public class RemoveExpenseRequest
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string ExpenseId { get; set; }
    }
}
