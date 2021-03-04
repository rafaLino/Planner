namespace Planner.Infrastructure.Entities
{
    public class AmountRecord : Entity
    {
        public string Description { get; set; }

        public decimal Amount { get; set; }

        public string FinanceStatementId { get; set; }
    }
}
