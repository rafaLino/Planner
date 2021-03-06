﻿using Planner.Domain.ValueObjects;
using System;

namespace Planner.Domain.Accounts
{
    public sealed class Investment : FinanceStatement, IEntity
    {
        public override Guid Id { get; protected set; }
        public override Title Title { get; protected set; }
        public override AmountRecordCollection AmountRecords { get; protected set; }
        public override MonthYear ReferenceDate { get; protected set; }

        public Investment(Title title) : base(title)
        {
        }

        public Investment(Title title, Amount amount) : base(title, amount)
        {
        }

        private Investment() { }

        public static Investment Load(Guid id, Title title, AmountRecordCollection amountRecords, MonthYear referenceDate)
        {
            Investment investment = new Investment();
            investment.Id = id;
            investment.Title = title;
            investment.AmountRecords = amountRecords;
            investment.ReferenceDate = referenceDate;
            return investment;
        }
    }
}
