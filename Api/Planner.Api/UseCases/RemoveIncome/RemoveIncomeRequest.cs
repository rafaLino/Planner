using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.RemoveIncome
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoveIncomeRequest
    {
        /// <summary>
        /// account id
        /// </summary>
        [Required]
        public System.Guid AccountId { get; set; }

        /// <summary>
        /// income id
        /// </summary>
        [Required]
        public System.Guid IncomeId { get; set; }
    }
}
