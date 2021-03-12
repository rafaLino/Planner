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
            Content = incomes.GetFinanceStatements().Select(income =>
                        {
                            return new FinanceStatementQueryResult(income.Id, income.Title, 0, income.AmountRecords);
                        });

        }
    }
}
