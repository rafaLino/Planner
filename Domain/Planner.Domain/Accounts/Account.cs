using Planner.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Planner.Domain.Accounts
{
    public sealed class Account : IEntity
    {
        public Guid Id { get; private set; }

        public bool Actived { get; private set; }

        public FinanceStatementCollection Incomes { get; private set; }

        public FinanceStatementCollection Expenses { get; private set; }

        public FinanceStatementCollection Investments { get; private set; }


        private Account() { }

        public Account(bool actived = true)
        {
            Id = Guid.NewGuid();
            Actived = actived;
            Incomes = new FinanceStatementCollection();
            Expenses = new FinanceStatementCollection();
            Investments = new FinanceStatementCollection();
        }

        public static Account Load(Guid id, bool actived, FinanceStatementCollection incomes, FinanceStatementCollection expenses, FinanceStatementCollection investments)
        {
            Account account = new Account();
            account.Id = id;
            account.Actived = actived;
            account.Incomes = incomes ?? new FinanceStatementCollection();
            account.Expenses = expenses ?? new FinanceStatementCollection();
            account.Investments = investments ?? new FinanceStatementCollection();
            return account;
        }


        public IFinanceStatement Get<T>(Func<IFinanceStatement, bool> predicate) where T : class, IFinanceStatement
        {
            if (IsExpense(typeof(T)))
                return Expenses.Get(predicate);

            else if (IsIncome(typeof(T)))
                return Incomes.Get(predicate);

            else if (IsInvestment(typeof(T)))
                return Investments.Get(predicate);

            throw new FinanceStatementNotFoundException("not found!");
        }

        public FinanceStatementCollection GetCollecion<T>() where T : class, IFinanceStatement
        {
            if (IsExpense(typeof(T)))
                return Expenses;

            else if (IsIncome(typeof(T)))
                return Incomes;

            else if (IsInvestment(typeof(T)))
                return Investments;

            throw new FinanceStatementCollectionNotFoundException("collection not found!");
        }

        public decimal CurrentBalance()
        {
            return Incomes.Total() - (Expenses.Total() + Investments.Total());
        }

        public IEnumerable<IFinanceStatement> GetAll()
        {
           return Incomes.GetFinanceStatements()
                   .Concat(Expenses.GetFinanceStatements())
                   .Concat(Investments.GetFinanceStatements());
        }

        public void UpdateId(Guid id)
        {
            Id = id;
        }

        private bool IsExpense(Type type)
        {
            return type == typeof(Expense);
        }

        private bool IsIncome(Type type)
        {
            return type == typeof(Income);
        }

        private bool IsInvestment(Type type)
        {
            return type == typeof(Investment);
        }
    }
}
