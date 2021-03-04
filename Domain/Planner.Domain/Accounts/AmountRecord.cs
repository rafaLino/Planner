using Planner.Domain.ValueObjects;

namespace Planner.Domain.Accounts
{
    public sealed class AmountRecord : IEntity
    {
        public string Id { get; private set; }

        public string Description { get; private set; }

        public Amount Amount { get; private set; }

        public AmountRecord(Amount amount)
        {
            Amount = amount;
        }

        public AmountRecord(Amount amount, string description)
        {
            Amount = amount;
            Description = description;
        }

        private AmountRecord() { }

        public static AmountRecord Load(string id, string description, Amount amount)
        {
            AmountRecord amountRecord = new AmountRecord();
            amountRecord.Id = id;
            amountRecord.Description = description;
            amountRecord.Amount = amount;
            return amountRecord;
        }
    }
}
