using Planner.Domain.ValueObjects;

namespace Planner.Domain.Accounts
{
    public sealed class Expense : FinanceStatement, IEntity
    {

        public override string Id { get; protected set; }
        public override Title Title { get; protected set; }
        public override AmountRecordCollection AmountRecords { get; protected set; }
        public override MonthYear ReferenceDate { get; protected set; }

        public Expense(Title title) : base(title)
        {
        }

        public Expense(Title title, Amount amount) : base(title, amount)
        {
        }

        private Expense() { }

        public static Expense Load(string id, Title title, AmountRecordCollection amountRecords, MonthYear referenceDate)
        {
            Expense expense = new Expense();
            expense.Id = id;
            expense.Title = title;
            expense.AmountRecords = amountRecords;
            expense.ReferenceDate = referenceDate;
            return expense;
        }

        public override void UpdateId(string id)
        {
            Id = id;
        }


    }
}
