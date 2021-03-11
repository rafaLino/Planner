using Planner.Domain.Accounts;
using System.Collections.Generic;
using System.Linq;

namespace Planner.Application.Results
{
    public class InvestmentCardResult
    {
        public IEnumerable<FinanceStatementQueryResult> Content { get; }

        public TotalQueryResult Total { get; }

        public InvestmentCardResult(FinanceStatementCollection investments, decimal totalIncome)
        {
            decimal total = investments.Total();
            double percent = investments.Percentage(totalIncome);

            Total = new TotalQueryResult(total, percent);

            Content = investments.GetFinanceStatements().Select(expense =>
            {
                return new FinanceStatementQueryResult(expense.Id, expense.Title, total, expense.AmountRecords);
            });

        }
    }
}
