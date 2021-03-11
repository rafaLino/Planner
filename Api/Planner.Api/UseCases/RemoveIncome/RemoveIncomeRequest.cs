using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.RemoveIncome
{
    public class RemoveIncomeRequest
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string IncomeId { get; set; }
    }
}
