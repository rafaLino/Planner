using System;

namespace Planner.Infrastructure.Entities
{
    public class AmountRecord
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public decimal Amount { get; set; }

        public Guid FinanceStatementId { get; set; }
    }
}
