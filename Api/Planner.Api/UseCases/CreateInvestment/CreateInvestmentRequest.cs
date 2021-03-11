using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.CreateInvestment
{
    public class CreateInvestmentRequest
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string Title { get; set; }

        public decimal? Amount { get; set; }
    }
}
