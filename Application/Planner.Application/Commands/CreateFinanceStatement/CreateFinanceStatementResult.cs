﻿using Planner.Application.Results;

namespace Planner.Application.Commands.CreateFinanceStatement
{
    public class CreateFinanceStatementResult
    {
        public string Id { get; set; }
        public decimal Total { get; set; }
        public double Percentage { get; set; }

        public FinanceStatementResult Income { get; set; }

        public FinanceStatementResult Expense { get; set; }

        public FinanceStatementResult Investment { get; set; }
    }
}
