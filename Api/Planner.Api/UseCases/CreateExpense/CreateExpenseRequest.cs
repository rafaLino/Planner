using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.CreateExpense
{
    public class CreateExpenseRequest
    {
        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public string Title { get; set; }

        public decimal? Amount { get; set; }
    }
}
