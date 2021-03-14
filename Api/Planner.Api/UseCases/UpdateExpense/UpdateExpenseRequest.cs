using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.UpdateExpense
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateExpenseRequest
    {
        /// <summary>
        /// account id
        /// </summary>
        [Required]
        public System.Guid AccountId { get; set; }

        /// <summary>
        /// expense id
        /// </summary>
        [Required]
        public System.Guid ExpenseId { get; set; }

        /// <summary>
        /// expense title
        /// </summary>
        [Required]
        public string Title { get; set; }

    }
}
