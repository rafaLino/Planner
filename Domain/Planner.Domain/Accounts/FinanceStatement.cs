using Planner.Domain.ValueObjects;

namespace Planner.Domain.Accounts
{
    public abstract class FinanceStatement : IFinanceStatement
    {
        public virtual Title Title { get; protected set; }

        public virtual AmountRecordCollection AmountRecords { get; protected set; }

        public virtual MonthYear ReferenceDate { get; protected set; }
        public abstract string Id { get; protected set; }

        protected FinanceStatement(Title title)
        {
            Title = title;
            ReferenceDate = MonthYear.Now;
            AmountRecords = new AmountRecordCollection();
        }

        protected FinanceStatement(Title title, Amount amount)
        {
            Title = title;
            ReferenceDate = MonthYear.Now;
            AmountRecords = new AmountRecordCollection();
            if (amount != null)
                AmountRecords.Add(amount);
        }

        protected FinanceStatement() { }

        public virtual void UpdateInfo(Title title)
        {
            Title = title;
        }
    }
}
