using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.RemoveInvestment
{
    public class RemoveInvestmentRequest
    {
        [Required]
        public System.Guid AccountId { get; set; }

        [Required]
        public System.Guid InvestmentId { get; set; }
    }
}
