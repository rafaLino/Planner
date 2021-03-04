using Planner.Domain.ValueObjects;

namespace Planner.Domain.Accounts
{
    public interface IFinanceStatement : IEntity
    {
        Title Title { get; }

        AmountRecordCollection AmountRecords { get; }

        MonthYear ReferenceDate { get; }


    }
}
