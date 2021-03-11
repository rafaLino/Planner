using Planner.Application.Results;

namespace Planner.Application.Commands.SaveAmountRecord
{
    public class SaveAmountRecordResult
    {
        public string Id { get; set; }
        public decimal Total { get; set; }
        public double Percentage { get; set; }
        public FinanceStatementResult Income { get; set; }
        public FinanceStatementResult Expense { get; set; }
        public FinanceStatementResult Investment { get; set; }
    }
}
