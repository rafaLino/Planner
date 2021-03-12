using Planner.Api.Model;
using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.SaveInvestmentAmountRecords
{
    public class SaveInvestmentAmountRecordRequest
    {
        [Required]
        public System.Guid AccountId { get; set; }

        [Required]
        public System.Guid InvestmentId { get; set; }

        [Required]
        public AmountRecordModel[] AmountRecords { get; set; }
    }
}
