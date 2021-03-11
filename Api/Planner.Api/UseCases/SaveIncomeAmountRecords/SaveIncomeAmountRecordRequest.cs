using Planner.Api.Model;
using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.SaveIncomeAmountRecords
{
    public class SaveIncomeAmountRecordRequest
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string IncomeId { get; set; }

        [Required]
        public AmountRecordModel[] AmountRecords { get; set; }
    }
}
