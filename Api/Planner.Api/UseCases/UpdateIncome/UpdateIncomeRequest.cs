using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.UpdateIncome
{
    public class UpdateIncomeRequest
    {
        [Required]
        public System.Guid AccountId { get; set; }

        [Required]
        public System.Guid IncomeId { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
