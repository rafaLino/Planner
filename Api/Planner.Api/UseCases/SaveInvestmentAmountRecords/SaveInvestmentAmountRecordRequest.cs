using Planner.Api.Model;
using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.SaveInvestmentAmountRecords
{
    /// <summary>
    /// 
    /// </summary>
    public class SaveInvestmentAmountRecordRequest
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

        /// <summary>
        /// invesment amount's list
        /// </summary>
        [Required]
        public AmountRecordModel[] AmountRecords { get; set; }
    }
}
