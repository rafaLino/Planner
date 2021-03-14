using Planner.Api.Model;
using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.SaveIncomeAmountRecords
{
    /// <summary>
    /// 
    /// </summary>
    public class SaveIncomeAmountRecordRequest
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
        /// income amount's list
        /// </summary>
        [Required]
        public AmountRecordModel[] AmountRecords { get; set; }
    }
}
