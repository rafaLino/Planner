using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.Api.Model
{
    public class AmountRecordModel
    {
        public Guid? Id { get; set; } = null;
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
