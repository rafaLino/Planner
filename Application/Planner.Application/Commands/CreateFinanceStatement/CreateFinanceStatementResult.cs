namespace Planner.Application.Commands.CreateFinanceStatement
{
    public class CreateFinanceStatementResult
    {
        public string Id { get; set; }
        public decimal Total { get; set; }
        public double Percentage { get; set; }
        public double ExpenseTotalPercentage { get; set; }
        public double InvestmentTotalPercentage { get; set; }
    }
}
