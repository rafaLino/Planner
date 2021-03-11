﻿using Planner.Domain.Accounts;
using Planner.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Planner.UseCases.Tests
{
    public class AccountBuilder
    {
        private string Id;

        private bool Actived;

        private FinanceStatementCollection Incomes;

        private FinanceStatementCollection Expenses;

        private FinanceStatementCollection Investments;

        private AccountBuilder()
        {
            Id = Guid.NewGuid().ToString();
            Actived = true;
            Incomes = new FinanceStatementCollection();
            Expenses = new FinanceStatementCollection();
            Investments = new FinanceStatementCollection();
        }

        public static AccountBuilder New => new AccountBuilder();

        public AccountBuilder WithId(string id)
        {
            Id = id;
            return this;
        }

        public AccountBuilder WithIncomes(int length = 2)
        {
            var list = new List<Income>();
            for (int i = 0; i < length; i++)
            {
                list.Add(Income.Load(Guid.NewGuid().ToString(), GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, GenerateDecimal()) }), MonthYear.Now));
            }
            Incomes = new FinanceStatementCollection(list);
            return this;
        }

        public AccountBuilder WithIncomes(params decimal[] values)
        {
            var list = new List<Income>();
            foreach (var value in values)
            {
                list.Add(Income.Load(Guid.NewGuid().ToString(), GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, value) }), MonthYear.Now));
            }
            Incomes = new FinanceStatementCollection(list);
            return this;
        }



        public AccountBuilder WithIncomes(string firstItemId, params decimal[] values)
        {
            var list = new List<Income>();
            list.Add(Income.Load(firstItemId, GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, values[0]) }), MonthYear.Now));

            for (int i = 1; i < values.Length; i++)
            {
                list.Add(Income.Load(Guid.NewGuid().ToString(), GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, values[i]) }), MonthYear.Now));
            }

            Incomes = new FinanceStatementCollection(list);
            return this;
        }

        public AccountBuilder WithExpenses(int length = 2)
        {
            var list = new List<Expense>();
            for (int i = 0; i < length; i++)
            {
                list.Add(Expense.Load(Guid.NewGuid().ToString(), GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, GenerateDecimal()) }), MonthYear.Now));
            }
            Expenses = new FinanceStatementCollection(list);
            return this;
        }
        public AccountBuilder WithExpenses(params decimal[] values)
        {
            var list = new List<Expense>();
            foreach (var value in values)
            {
                list.Add(Expense.Load(Guid.NewGuid().ToString(), GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, value) }), MonthYear.Now));
            }
            Expenses = new FinanceStatementCollection(list);
            return this;
        }

        public AccountBuilder WithExpenses(string firstItemId, int length = 3)
        {
            var list = new List<Expense>();
            list.Add(Expense.Load(firstItemId, GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, GenerateDecimal()) }), MonthYear.Now));
            for (int i = 0; i < length - 1; i++)
            {
                list.Add(Expense.Load(Guid.NewGuid().ToString(), GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, GenerateDecimal()) }), MonthYear.Now));
            }

            Expenses = new FinanceStatementCollection(list);
            return this;
        }

        public AccountBuilder WithExpenses(string firstItemId, params decimal[] values)
        {
            var list = new List<Expense>();
            list.Add(Expense.Load(firstItemId, GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, values[0]) }), MonthYear.Now));

            for (int i = 1; i < values.Length; i++)
            {
                list.Add(Expense.Load(Guid.NewGuid().ToString(), GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, values[i]) }), MonthYear.Now));
            }

            Expenses = new FinanceStatementCollection(list);
            return this;
        }

        public AccountBuilder WithExpenses(Title title, int length = 3)
        {
            var list = new List<Expense>();
            list.Add(Expense.Load(Guid.NewGuid().ToString(), title, new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, GenerateDecimal()) }), MonthYear.Now));
            for (int i = 0; i < length - 1; i++)
            {
                list.Add(Expense.Load(Guid.NewGuid().ToString(), GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, GenerateDecimal()) }), MonthYear.Now));
            }

            Expenses = new FinanceStatementCollection(list);
            return this;
        }

        public AccountBuilder WithExpense(string expenseId, string firstAmountRecordId, params decimal[] amountRecordValues)
        {
            Expense expense = new Expense(GenerateName(5));
            expense.UpdateId(expenseId);
            expense.AmountRecords.Add(AmountRecord.Load(firstAmountRecordId, GenerateName(5), amountRecordValues[0]));
            for (int i = 1; i < amountRecordValues.Length; i++)
            {
                expense.AmountRecords.Add(AmountRecord.Load(Guid.NewGuid().ToString(), GenerateName(5), amountRecordValues[i]));
            }
            Expenses.Add(expense);
            return this;

        }

        public AccountBuilder WithIncome(string incomeId, string firstAmountRecordId, params decimal[] amountRecordValues)
        {
            Income income = new Income(GenerateName(5));
            income.UpdateId(incomeId);
            income.AmountRecords.Add(AmountRecord.Load(firstAmountRecordId, GenerateName(5), amountRecordValues[0]));
            for (int i = 1; i < amountRecordValues.Length; i++)
            {
                income.AmountRecords.Add(AmountRecord.Load(Guid.NewGuid().ToString(), GenerateName(5), amountRecordValues[i]));
            }
            Incomes.Add(income);
            return this;

        }

        public AccountBuilder WithInvestment(string investmentId, string firstAmountRecordId, params decimal[] amountRecordValues)
        {
            Investment investment = new Investment(GenerateName(5));
            investment.UpdateId(investmentId);
            investment.AmountRecords.Add(AmountRecord.Load(firstAmountRecordId, GenerateName(5), amountRecordValues[0]));
            for (int i = 1; i < amountRecordValues.Length; i++)
            {
                investment.AmountRecords.Add(AmountRecord.Load(Guid.NewGuid().ToString(), GenerateName(5), amountRecordValues[i]));
            }
            Investments.Add(investment);
            return this;

        }

        public AccountBuilder WithInvestments(int length = 2)
        {
            var list = new List<Investment>();
            for (int i = 0; i < length; i++)
            {
                list.Add(Investment.Load(Guid.NewGuid().ToString(), GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, GenerateDecimal()) }), MonthYear.Now));
            }
            Investments = new FinanceStatementCollection(list);
            return this;
        }
        public AccountBuilder WithInvestments(params decimal[] values)
        {
            var list = new List<Investment>();
            foreach (var value in values)
            {
                list.Add(Investment.Load(Guid.NewGuid().ToString(), GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, value) }), MonthYear.Now));
            }
            Investments = new FinanceStatementCollection(list);
            return this;
        }

        public AccountBuilder WithInvestments(string firstItemId, int length = 3)
        {
            var list = new List<Investment>();
            list.Add(Investment.Load(firstItemId, GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, GenerateDecimal()) }), MonthYear.Now));
            for (int i = 0; i < length - 1; i++)
            {
                list.Add(Investment.Load(Guid.NewGuid().ToString(), GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, GenerateDecimal()) }), MonthYear.Now));
            }

            Investments = new FinanceStatementCollection(list);
            return this;
        }

        public AccountBuilder WithInvestments(string firstItemId, params decimal[] values)
        {
            var list = new List<Investment>();
            list.Add(Investment.Load(firstItemId, GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, values[0]) }), MonthYear.Now));

            for (int i = 1; i < values.Length; i++)
            {
                list.Add(Investment.Load(Guid.NewGuid().ToString(), GenerateName(9), new AmountRecordCollection(new[] { AmountRecord.Load(Guid.NewGuid().ToString(), null, values[i]) }), MonthYear.Now));
            }

            Investments = new FinanceStatementCollection(list);
            return this;
        }

        public Account Build()
        {
            return Account.Load(Id, Actived, Incomes, Expenses, Investments);
        }



        #region Generates
        private string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }

        private decimal GenerateDecimal()
        {
            return Convert.ToDecimal(string.Format("{0:0.##}", new Random().NextDouble() * 10));
        }

        #endregion
    }
}
