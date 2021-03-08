using Planner.Domain.ValueObjects;

namespace Planner.Application.Commands.UpdateAmountRecord
{
    public class UpdateAmountRecordCommand
    {
        public string AccountId { get; set; }

        public string FinanceStatementId { get; set; }

        public string AmountRecordId { get; set; }

        public Amount Amount { get; set; }

        public string Description { get; set; } = null;
    }
}
