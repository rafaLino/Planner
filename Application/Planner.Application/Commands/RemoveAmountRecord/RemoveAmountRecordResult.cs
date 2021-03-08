using Planner.Application.Results;

namespace Planner.Application.Commands.RemoveAmountRecord
{
    public class RemoveAmountRecordResult
    {
        public FinanceStatementResult Income { get; set; }
        public FinanceStatementResult Expense { get; set; }
        public FinanceStatementResult Investment { get; set; }
    }
}
