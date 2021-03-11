namespace Planner.Application.Results
{
    public class AmountRecordQueryResult
    {
        public AmountRecordQueryResult(string id, decimal amount, string description)
        {
            Id = id;
            Amount = amount;
            Description = description;
        }

        public string Id { get; set; }
        public decimal Amount { get; }

        public string Description { get; }
    }
}
