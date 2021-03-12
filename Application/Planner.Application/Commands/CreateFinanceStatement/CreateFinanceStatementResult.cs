using Planner.Application.Results;
using System;
using System.Collections.Generic;

namespace Planner.Application.Commands.CreateFinanceStatement
{
    public class CreateFinanceStatementResult
    {
        public Guid Id { get; set; }
        public decimal Total { get; set; }
        public double Percentage { get; set; }

        public IEnumerable<AmountRecordResult> AmountRecords { get; set; }

        public FinanceStatementResult Income { get; set; }

        public FinanceStatementResult Expense { get; set; }

        public FinanceStatementResult Investment { get; set; }
    }
}
