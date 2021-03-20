using System.ComponentModel.DataAnnotations;

namespace Planner.Api.Model
{
    public class SaveAmountRecordRequest
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
        public System.Guid Id { get; set; }

        /// <summary>
        /// expense amount's list
        /// </summary>
        [Required]
        public AmountRecordModel[] AmountRecords { get; set; }
    }
}
