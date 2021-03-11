using Planner.Domain.Accounts;
using System.Collections.Generic;
using System.Linq;

namespace Planner.Application.Results
{
    public class FinanceStatementQueryResult
    {
        public FinanceStatementQueryResult(string id, string title, decimal financeStatementTotal, AmountRecordCollection amountRecords)
        {
            Id = id;
            Title = title;
            Total = amountRecords.Total();
            Percentage = amountRecords.Percentage(financeStatementTotal);

            AmountRecords = amountRecords.GetAmountRecords()
                .Select(amountRecord =>
            {
                return new AmountRecordQueryResult(amountRecord.Id, amountRecord.Amount, amountRecord.Description);
            });

        }

        public string Id { get; set; }
        public string Title { get; }
        public decimal Total { get; }
        public double Percentage { get; }

        public IEnumerable<AmountRecordQueryResult> AmountRecords { get; }


    }
}
