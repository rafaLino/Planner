using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.UpdateInvestment
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateInvestmentRequest
    {
        /// <summary>
        /// account id
        /// </summary>
        [Required]
        public System.Guid AccountId { get; set; }

        /// <summary>
        /// invetment id
        /// </summary>
        [Required]
        public System.Guid InvestmentId { get; set; }

        /// <summary>
        /// title
        /// </summary>
        [Required]
        public string Title { get; set; }
    }
}
