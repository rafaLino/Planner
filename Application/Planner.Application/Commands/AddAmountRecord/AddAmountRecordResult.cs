using Planner.Application.Results;

namespace Planner.Application.Commands.AddAmountRecord
{
    public class AddAmountRecordResult
    {
        public string Id { get; set; }
        public decimal Total { get; set; }
        public double Percentage { get; set; }
        public FinanceStatementResult Income { get; set; }
        public FinanceStatementResult Expense { get; set; }
        public FinanceStatementResult Investment { get; set; }
    }
}
