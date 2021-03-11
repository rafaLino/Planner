using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.UpdateExpense
{
    public class UpdateExpenseRequest
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string ExpenseId { get; set; }

        [Required]
        public string Title { get; set; }

    }
}
