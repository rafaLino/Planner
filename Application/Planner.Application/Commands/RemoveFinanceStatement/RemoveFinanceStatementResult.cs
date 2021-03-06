﻿using Planner.Application.Results;

namespace Planner.Application.Commands.RemoveFinanceStatement
{
    public class RemoveFinanceStatementResult
    {
        public FinanceStatementResult Income { get; set; }

        public FinanceStatementResult Expense { get; set; }

        public FinanceStatementResult Investment { get; set; }
    }
}
