using Planner.Domain.ValueObjects;
using System;

namespace Planner.Domain.Accounts
{
    public sealed class AmountRecord : IEntity
    {
        public Guid Id { get; private set; }

        public string Description { get; private set; }

        public Amount Amount { get; private set; }

        public AmountRecord(Amount amount)
        {
            Id = Guid.NewGuid();
            Amount = amount;
        }

        public AmountRecord(Amount amount, string description)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            Description = description;
        }

        private AmountRecord() { }


        public void Update(Amount amount, string description)
        {
            Amount = amount ?? Amount;
            Description = description ?? Description;
        }

        public static AmountRecord Load(Guid? id, string description, Amount amount)
        {
            AmountRecord amountRecord = new AmountRecord();
            amountRecord.Id = id ?? Guid.NewGuid();
            amountRecord.Description = description;
            amountRecord.Amount = amount;
            return amountRecord;
        }


    }
}
