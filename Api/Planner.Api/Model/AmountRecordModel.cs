using System.ComponentModel.DataAnnotations;

namespace Planner.Api.Model
{
    public class AmountRecordModel
    {
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
