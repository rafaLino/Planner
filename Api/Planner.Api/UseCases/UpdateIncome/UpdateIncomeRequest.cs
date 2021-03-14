using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.UpdateIncome
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateIncomeRequest
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

        /// <summary>
        /// income title
        /// </summary>
        [Required]
        public string Title { get; set; }
    }
}
