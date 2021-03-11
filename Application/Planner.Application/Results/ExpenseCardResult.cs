using Planner.Domain.Accounts;
using System.Collections.Generic;
using System.Linq;

namespace Planner.Application.Results
{
    public class ExpenseCardResult
    {
        public IEnumerable<FinanceStatementQueryResult> Content { get; }

        public TotalQueryResult Total { get; }

        public ExpenseCardResult(FinanceStatementCollection expenses, decimal totalIncome)
        {
            decimal total = expenses.Total();
            double percent = expenses.Percentage(totalIncome);

            Total = new TotalQueryResult(total, percent);

            Content = expenses.GetFinanceStatements().Select(expense =>
            {
                return new FinanceStatementQueryResult(expense.Id, expense.Title, total, expense.AmountRecords);
            });

        }
    }
}
