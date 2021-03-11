namespace Planner.Application.Results
{
    public class TotalQueryResult
    {
        public TotalQueryResult(decimal amount, double percentage)
        {
            Amount = amount;
            Percentage = percentage;
        }

        public decimal Amount { get; }

        public double Percentage { get; }
    }
}
