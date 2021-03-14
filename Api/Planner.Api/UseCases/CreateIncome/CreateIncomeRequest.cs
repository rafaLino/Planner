using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.CreateIncome
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateIncomeRequest
    {
        /// <summary>
        /// Account Id
        /// </summary>
        [Required]
        public System.Guid AccountId { get; set; }

        /// <summary>
        /// Income's name
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// income's amount
        /// </summary>
        public decimal? Amount { get; set; }
    }
}
