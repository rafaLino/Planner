using Planner.Api.Model;
using System.ComponentModel.DataAnnotations;

namespace Planner.Api.UseCases.SaveInvestmentAmountRecords
{
    public class SaveInvestmentAmountRecordRequest
    {
        [Required]
        public string AccountId { get; set; }

        [Required]
        public string InvestmentId { get; set; }

        [Required]
        public AmountRecordModel[] AmountRecords { get; set; }
    }
}
