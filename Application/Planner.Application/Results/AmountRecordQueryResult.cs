namespace Planner.Application.Results
{
    public class AmountRecordQueryResult
    {
        public AmountRecordQueryResult(System.Guid id, decimal amount, string description)
        {
            Id = id;
            Amount = amount;
            Description = description;
        }

        public System.Guid Id { get; set; }
        public decimal Amount { get; }

        public string Description { get; }
    }
}
