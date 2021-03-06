using Planner.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Planner.Domain.Accounts
{
    public class FinanceStatementCollection
    {
        private readonly IList<IFinanceStatement> _financeStatements;

        public FinanceStatementCollection()
        {
            _financeStatements = new List<IFinanceStatement>();
        }

        public FinanceStatementCollection(IEnumerable<IFinanceStatement> financeStatements)
        {
            _financeStatements = new List<IFinanceStatement>(financeStatements);
        }

        public IReadOnlyCollection<IFinanceStatement> GetFinanceStatements()
        {
            IReadOnlyCollection<IFinanceStatement> statements = new ReadOnlyCollection<IFinanceStatement>(_financeStatements);
            return statements;
        }

        public void Add(IFinanceStatement item)
        {
            if (_financeStatements.Any(x => x.Title == item.Title))
                throw new FinanceStatementExistsException($"{item.Title} already exists!");

            _financeStatements.Add(item);
        }

        public IFinanceStatement Get(string id)
        {
            return _financeStatements.SingleOrDefault(x => x.Id == id);
        }

        public void Remove(IFinanceStatement item)
        {
            _financeStatements.Remove(item);
        }

        public decimal Total()
        {
            return _financeStatements.Sum(x => x.AmountRecords.Total());
        }

        public double Percentage(decimal total)
        {
            return Convert.ToDouble(Total() * 100 / total);
        }
    }
}
