﻿using Planner.Domain.Accounts;
using System;

namespace Planner.Application.Results
{
    public class AccountQueryResult
    {
        public string AccountId { get; }

        public decimal CurrentBalance { get; }

        public ExpenseCardResult Expenses { get; }
        public IncomeCardResult Incomes { get; }
        public InvestmentCardResult Investments { get; }


        public AccountQueryResult(string accountId, FinanceStatementCollection expenses, FinanceStatementCollection incomes, FinanceStatementCollection investments)
        {
            decimal totalIncomes = incomes.Total();
            AccountId = accountId;
            CurrentBalance = totalIncomes - expenses.Total() + investments.Total();

            Incomes = new IncomeCardResult(incomes);
            Expenses = new ExpenseCardResult(expenses, incomes.Total());
            Investments = new InvestmentCardResult(investments, totalIncomes);
        }


    }
}
