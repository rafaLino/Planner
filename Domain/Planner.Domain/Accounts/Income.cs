using Planner.Domain.ValueObjects;

namespace Planner.Domain.Accounts
{
    public sealed class Income : FinanceStatement, IEntity
    {


        public override string Id { get; protected set; }
        public override Title Title { get; protected set; }

        public override AmountRecordCollection AmountRecords { get; protected set; }

        public override MonthYear ReferenceDate { get; protected set; }

        public Income(Title title) : base(title)
        {
        }

        public Income(Title title, Amount amount) : base(title, amount)
        {
        }

        private Income() { }

        public static Income Load(string id, Title title, AmountRecordCollection amountRecords, MonthYear referenceDate)
        {
            Income income = new Income();
            income.Id = id;
            income.Title = title;
            income.AmountRecords = amountRecords;
            income.ReferenceDate = referenceDate;
            return income;
        }

    }
}
