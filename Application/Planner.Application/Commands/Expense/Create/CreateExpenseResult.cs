﻿namespace Planner.Application.Commands.Expense.Create
{
    public class CreateExpenseResult
    {
        public string Id { get; set; }
        public decimal Total { get; set; }
        public double Percentage { get; set; }

        public double TotalPercentage { get; set; }

    }
}
