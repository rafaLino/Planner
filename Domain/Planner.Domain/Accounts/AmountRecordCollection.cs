using Planner.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Planner.Domain.Accounts
{
    public class AmountRecordCollection
    {
        private readonly IList<AmountRecord> _amountRecords;

        public AmountRecordCollection()
        {
            _amountRecords = new List<AmountRecord>();
        }

        public AmountRecordCollection(IEnumerable<AmountRecord> records)
        {
            _amountRecords = new List<AmountRecord>(records);
        }

        public IReadOnlyCollection<AmountRecord> GetAmountRecords()
        {
            IReadOnlyCollection<AmountRecord> records = new ReadOnlyCollection<AmountRecord>(_amountRecords);
            return records;
        }


        public void Add(Amount amount, string description = null)
        {
            AmountRecord record = new AmountRecord(amount, description);
            _amountRecords.Add(record);
        }

        public void Add(AmountRecord amount)
        {
            _amountRecords.Add(amount);
        }

        public Amount Total()
        {
            return _amountRecords.Sum(x => x.Amount);
        }

        public double Percentage(decimal total)
        {
            return Convert.ToDouble(Total() * 100 / total);
        }
    }
}
