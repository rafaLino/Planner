using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.RemoveInvestment
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoveInvestmentRequest
    {
        /// <summary>
        /// account id
        /// </summary>
        [Required]
        public System.Guid AccountId { get; set; }

        /// <summary>
        /// investment id
        /// </summary>
        [Required]
        public System.Guid InvestmentId { get; set; }
    }
}
