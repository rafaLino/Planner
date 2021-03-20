using Planner.Domain.Accounts;

namespace Planner.Application.Results
{
    public class AccountQueryResult
    {
        public System.Guid AccountId { get; }

        public decimal CurrentBalance { get; }

        public ExpenseCardResult Expenses { get; }
        public IncomeCardResult Incomes { get; }
        public InvestmentCardResult Investments { get; }


        public AccountQueryResult(System.Guid accountId, FinanceStatementCollection expenses, FinanceStatementCollection incomes, FinanceStatementCollection investments)
        {
            decimal totalIncomes = incomes.Total();
            AccountId = accountId;
            CurrentBalance = totalIncomes - expenses.Total() + investments.Total();

            Incomes = new IncomeCardResult(incomes);
            Expenses = new ExpenseCardResult(expenses, totalIncomes);
            Investments = new InvestmentCardResult(investments, totalIncomes);
        }


    }
}
