namespace Planner.Application.Commands
{
    public class CreateFinanceStatementResult
    {
        public string Id { get; set; }
        public double Percentage { get; set; }
        public decimal Total { get; set; }
        public double ExpenseTotalPercentage { get; set; }
        public double InvestmentTotalPercentage { get; set; }
    }
}
