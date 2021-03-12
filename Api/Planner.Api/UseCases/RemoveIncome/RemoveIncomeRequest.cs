using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.RemoveIncome
{
    public class RemoveIncomeRequest
    {
        [Required]
        public System.Guid AccountId { get; set; }

        [Required]
        public System.Guid IncomeId { get; set; }
    }
}
