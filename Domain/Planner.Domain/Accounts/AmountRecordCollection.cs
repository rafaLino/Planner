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

        public void Replace(IEnumerable<AmountRecord> amountRecords)
        {
            _amountRecords.Clear();
            foreach (var item in amountRecords)
                _amountRecords.Add(item);
        }

        public AmountRecord Get(string id)
        {
            return _amountRecords.FirstOrDefault(x => x.Id == id);
        }

        public AmountRecord Remove(string id)
        {
            AmountRecord amountRecord = Get(id);
            bool removed = _amountRecords.Remove(amountRecord);
            if (removed)
                return amountRecord;

            return default;
        }

        public Amount Total()
        {
            return _amountRecords.Sum(x => x.Amount);
        }

        public double Percentage(decimal total)
        {
            decimal totalRecords = Total();
            if (totalRecords == 0 || total == 0)
                return 0;

            return Convert.ToDouble(totalRecords / total);
        }
    }
}
