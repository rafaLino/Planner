using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.CreateExpense
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateExpenseRequest
    {
        /// <summary>
        /// account id
        /// </summary>
        [Required]
        public Guid AccountId { get; set; }

        /// <summary>
        /// expense's title
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// expense's amount
        /// </summary>
        public decimal? Amount { get; set; }
    }
}
