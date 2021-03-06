﻿using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Expenses.Remove
{
    public class RemoveExpenseUseCase : IRemoveExpenseUseCase
    {
        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;

        public RemoveExpenseUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<RemoveFinanceStatementResult> Execute(string accountId, string expenseId)
        {
            Account account = await _accountReadOnlyRepository.Get(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists");

            Expense expense = (Expense)account
                                         .Expenses
                                         .Get(expenseId);

            account.Expenses.Remove(expense);

            await _accountWriteOnlyRepository.Remove(account, expense);

            RemoveFinanceStatementResult result = new RemoveFinanceStatementResult
            {
                Total = account.Expenses.Total(),
                ExpenseTotalPercentage = account.Expenses.Percentage(account.Incomes.Total()),
                InvestmentTotalPercentage = account.Investments.Percentage(account.Incomes.Total())
            };

            return result;
        }
    }
}
