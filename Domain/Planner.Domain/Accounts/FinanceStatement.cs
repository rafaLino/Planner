using Planner.Domain.ValueObjects;
using System;

namespace Planner.Domain.Accounts
{
    public abstract class FinanceStatement : IFinanceStatement
    {
        public virtual Title Title { get; protected set; }

        public virtual AmountRecordCollection AmountRecords { get; protected set; }

        public virtual MonthYear ReferenceDate { get; protected set; }
        public abstract Guid Id { get; protected set; }

        protected FinanceStatement(Title title)
        {
            Id = Guid.NewGuid();
            Title = title;
            ReferenceDate = MonthYear.Now;
            AmountRecords = new AmountRecordCollection();
        }

        protected FinanceStatement(Title title, Amount amount)
        {
            Id = Guid.NewGuid();
            Title = title;
            ReferenceDate = MonthYear.Now;
            AmountRecords = new AmountRecordCollection();
            if (amount != null)
                AmountRecords.Add(amount);
        }

        protected FinanceStatement() { }

        public virtual void Update(Title title)
        {
            Title = title;
        }

    }
}
