using Planner.Domain.Accounts;
using System.Collections.Generic;
using System.Linq;

namespace Planner.Application.Results
{
    public class IncomeCardResult
    {
        public IEnumerable<FinanceStatementQueryResult> Content { get; }

        public IncomeCardResult(FinanceStatementCollection incomes)
        {
            Content = incomes.GetFinanceStatements().Select(expense =>
                        {
                            return new FinanceStatementQueryResult(expense.Id, expense.Title, 0, expense.AmountRecords);
                        });

        }
    }
}
