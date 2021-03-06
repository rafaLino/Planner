﻿using Planner.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Planner.Application.Commands.Expenses.Create
{
    public interface ICreateExpenseUseCase
    {
        Task<CreateFinanceStatementResult> Execute(string accountId, Title title, Amount amount = null);
    }
}
