using Planner.Domain.Accounts;
using System;
using Xunit;

namespace Planner.Domain.Tests
{
    public class AccountTests
    {

        [Fact]
        public void Should_Add_Expense()
        {
            Account account = new Account();

            Expense expense = new Expense("despesas");

            account.GetCollecion<Expense>().Add(expense);

            var result = account.Expenses.GetFinanceStatements();
            Assert.Single(result);
        }

        [Fact]
        public void Should_Add_Income()
        {
            Account account = new Account();

            Income income = new Income("bonus");

            account.GetCollecion<Income>().Add(income);

            var result = account.Incomes.GetFinanceStatements();
            Assert.Single(result);
        }

        [Fact]
        public void Should_Add_Investment()
        {
            Account account = new Account();

            Investment investment = new Investment("BDR");

            account.GetCollecion<Investment>().Add(investment);

            var result = account.Investments.GetFinanceStatements();
            Assert.Single(result);
        }

        [Fact]
        public void Should_Get_Expense()
        {
            string expenseId = Guid.NewGuid().ToString();
            Account account = new Account();

            Expense expense = new Expense("despesas");
            expense.UpdateId(expenseId);

            account.GetCollecion<Expense>().Add(expense);

            var result = account.Get<Expense>(x => x.Id == expenseId);

            Assert.Equal(expense, result);
            Assert.IsType<Expense>(result);
        }

        [Fact]
        public void Should_Get_Income()
        {
            string incomeId = Guid.NewGuid().ToString();
            Account account = new Account();

            Income income = new Income("bonus");
            income.UpdateId(incomeId);

            account.GetCollecion<Income>().Add(income);

            var result = account.Get<Income>(x => x.Id == incomeId);

            Assert.Equal(income, result);
            Assert.IsType<Income>(result);
        }

        [Fact]
        public void Should_Get_Investment()
        {
            string investmentId = Guid.NewGuid().ToString();
            Account account = new Account();

            Investment investment = new Investment("bonus");
            investment.UpdateId(investmentId);

            account.GetCollecion<Investment>().Add(investment);

            var result = account.Get<Investment>(x => x.Id == investmentId);

            Assert.Equal(investment, result);
            Assert.IsType<Investment>(result);
        }
    }
}
