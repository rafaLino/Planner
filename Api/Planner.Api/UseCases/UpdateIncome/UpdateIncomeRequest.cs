using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.UpdateIncome
{
    public class UpdateIncomeRequest
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string IncomeId { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
