using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.UpdateExpense
{
    public class UpdateExpenseRequest
    {
        [Required]
        public System.Guid AccountId { get; set; }

        [Required]
        public System.Guid ExpenseId { get; set; }

        [Required]
        public string Title { get; set; }

    }
}
