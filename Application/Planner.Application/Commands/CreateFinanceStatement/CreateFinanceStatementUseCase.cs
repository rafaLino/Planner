﻿using Planner.Application.Exceptions;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using Planner.Domain.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Application.Commands.CreateFinanceStatement
{
    public class CreateFinanceStatementUseCase : ICreateFinanceStatementUseCase
    {
        private readonly IAccountReadOnlyRepository _accountReadOnlyRepository;

        private readonly IAccountWriteOnlyRepository _accountWriteOnlyRepository;

        public CreateFinanceStatementUseCase(IAccountReadOnlyRepository accountReadOnlyRepository, IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            _accountReadOnlyRepository = accountReadOnlyRepository;
            _accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<CreateFinanceStatementResult> Execute<T>(Guid accountId, Title title, Amount amount = null) where T : class, IFinanceStatement
        {
            Account account = await _accountReadOnlyRepository.Get(accountId);

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists!");

            T financeStatement = (T)Activator.CreateInstance(typeof(T), title, amount);

            FinanceStatementCollection collection = account
                 .GetCollecion<T>();

            collection.Add(financeStatement);

            await _accountWriteOnlyRepository.Update(account, financeStatement);

            decimal totalIncomes = account.Incomes.Total();
            decimal totalExpenses = account.Expenses.Total();
            decimal totalInvestments = account.Investments.Total();

            CreateFinanceStatementResult result = new CreateFinanceStatementResult
            {
                Id = financeStatement.Id,
                Total = financeStatement.AmountRecords.Total(),
                Percentage = financeStatement.AmountRecords.Percentage(collection.Total()),
                AmountRecords = financeStatement.AmountRecords.GetAmountRecords().Select(x => new Results.AmountRecordResult
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Description = x.Description
                }),
                Income = new Results.FinanceStatementResult
                {
                    Total = totalIncomes
                },
                Expense = new Results.FinanceStatementResult
                {
                    Total = totalExpenses,
                    Percentage = account.Expenses.Percentage(totalIncomes)
                },
                Investment = new Results.FinanceStatementResult
                {
                    Total = totalInvestments,
                    Percentage = account.Investments.Percentage(totalIncomes)
                },
            };

            return result;
        }
    }
}
