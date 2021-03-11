using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.CreateIncome
{
    public class CreateIncomeRequest
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string Title { get; set; }

        public decimal? Amount { get; set; }
    }
}
