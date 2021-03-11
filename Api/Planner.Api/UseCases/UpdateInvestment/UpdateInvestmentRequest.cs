using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.UpdateInvestment
{
    public class UpdateInvestmentRequest
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string InvestmentId { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
