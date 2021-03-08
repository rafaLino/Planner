using Planner.Application.Results;

namespace Planner.Application.Commands.UpdateAmountRecord
{
    public class UpdateAmountRecordResult
    {
        public decimal Total { get; set; }
        public double Percentage { get; set; }

        public FinanceStatementResult Income { get; set; }
        public FinanceStatementResult Expense { get; set; }
        public FinanceStatementResult Investment { get; set; }
    }
}
