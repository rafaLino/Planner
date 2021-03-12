using System;

namespace Planner.Application.Results
{
    public class AmountRecordResult
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }
    }
}
