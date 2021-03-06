﻿using System;

namespace Planner.Domain.Accounts
{
    public sealed class Account : IEntity
    {
        public string Id { get; private set; }

        public bool Actived { get; private set; }

        public FinanceStatementCollection Incomes { get; private set; }

        public FinanceStatementCollection Expenses { get; private set; }

        public FinanceStatementCollection Investments { get; private set; }



        private Account() { }

        public Account(bool actived = true)
        {
            Actived = actived;
            Incomes = new FinanceStatementCollection();
            Expenses = new FinanceStatementCollection();
            Investments = new FinanceStatementCollection();
        }

        public static Account Load(string id, bool actived, FinanceStatementCollection incomes, FinanceStatementCollection expenses, FinanceStatementCollection investments)
        {
            Account account = new Account();
            account.Id = id;
            account.Actived = actived;
            account.Incomes = incomes ?? new FinanceStatementCollection();
            account.Expenses = expenses ?? new FinanceStatementCollection();
            account.Investments = investments ?? new FinanceStatementCollection();
            return account;
        }

        public decimal CurrentBalance()
        {
            return Incomes.Total() - (Expenses.Total() + Investments.Total());
        }
    }
}
