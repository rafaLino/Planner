using Planner.Api.Model;
using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.SaveIncomeAmountRecords
{
    public class SaveIncomeAmountRecordRequest
    {
        [Required]
        public System.Guid AccountId { get; set; }

        [Required]
        public System.Guid IncomeId { get; set; }

        [Required]
        public AmountRecordModel[] AmountRecords { get; set; }
    }
}
