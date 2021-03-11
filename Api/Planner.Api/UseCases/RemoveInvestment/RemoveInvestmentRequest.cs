using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.RemoveInvestment
{
    public class RemoveInvestmentRequest
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string InvestmentId { get; set; }
    }
}
