using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.UpdateInvestment
{
    public class UpdateInvestmentRequest
    {
        [Required]
        public System.Guid AccountId { get; set; }

        [Required]
        public System.Guid InvestmentId { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
