﻿using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Expenses.Create
{
    public class CreateExpenseUseCase : ICreateExpenseUseCase
    {

        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;

        public CreateExpenseUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<CreateFinanceStatementResult> Execute(string accountId, Title title, Amount amount = null)
        {
            Account account = await _accountReadOnlyRepository.Get(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists!");

            Expense expense = new Expense(title, amount);

            account
                .Expenses
                .Add(expense);

            await _accountWriteOnlyRepository.Update(account);

            CreateFinanceStatementResult result = new CreateFinanceStatementResult
            {
                Id = expense.Id,
                Percentage = expense.AmountRecords.Percentage(account.Expenses.Total()),
                Total = account.Expenses.Total(),
                ExpenseTotalPercentage = account.Expenses.Percentage(account.Incomes.Total()),
                InvestmentTotalPercentage = account.Investments.Percentage(account.Incomes.Total())
            };

            return result;

        }
    }
}
