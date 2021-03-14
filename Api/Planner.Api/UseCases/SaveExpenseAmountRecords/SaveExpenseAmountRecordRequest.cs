using Planner.Api.Model;
using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.SaveExpenseAmountRecords
{
    /// <summary>
    /// 
    /// </summary>
    public class SaveExpenseAmountRecordRequest
    {
        /// <summary>
        /// account id
        /// </summary>
        [Required]
        public System.Guid AccountId { get; set; }

        /// <summary>
        /// expense id
        /// </summary>
        [Required]
        public System.Guid ExpenseId { get; set; }

        /// <summary>
        /// expense amount's list
        /// </summary>
        [Required]
        public AmountRecordModel[] AmountRecords { get; set; }
    }
}
