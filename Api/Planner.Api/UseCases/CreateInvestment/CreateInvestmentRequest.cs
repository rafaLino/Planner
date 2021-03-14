using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.CreateInvestment
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateInvestmentRequest
    {
        /// <summary>
        /// account id
        /// </summary>
        [Required]
        public System.Guid AccountId { get; set; }

        /// <summary>
        /// investment's title
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// investment's amount
        /// </summary>
        public decimal? Amount { get; set; }
    }
}
