using MongoDB.Driver;
using Planner.Application.Queries;
using Planner.Application.Results;
using Planner.Domain.Accounts;
using Planner.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Infrastructure.Queries
{
    public class AccountQueries : IAccountQueries
    {
        private readonly Context _context;

        public AccountQueries(Context context)
        {
            _context = context;
        }

        public async Task<AccountQueryResult> GetAccount(System.Guid accountId)
        {
            Entities.Account account = await _context
                .Accounts
                .Find(x => x.Id == accountId)
                .SingleOrDefaultAsync();

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists");

            if (!account.Actived)
                throw new AccountDeactivatedException($"The account {accountId} is deactivated!");

            IEnumerable<Entities.FinanceStatement> incomes = await _context
               .Incomes
               .Find(x => x.AccountId == accountId)
               .ToListAsync();

            IEnumerable<Entities.FinanceStatement> expenses = await _context
                .Expenses
                .Find(x => x.AccountId == accountId)
                .ToListAsync();

            IEnumerable<Entities.FinanceStatement> investments = await _context
                .Investments
                .Find(x => x.AccountId == accountId)
                .ToListAsync();


            FinanceStatementCollection incomeCollection = new FinanceStatementCollection();
            foreach (var income in incomes)
            {
                IEnumerable<Entities.AmountRecord> amountRecords = await _context
                    .AmountRecords
                    .Find(x => x.FinanceStatementId == income.Id)
                    .ToListAsync();

                AmountRecordCollection amountRecordCollection = new AmountRecordCollection(amountRecords.Select(x => AmountRecord.Load(x.Id, x.Description, x.Amount)));

                incomeCollection.Add(Income.Load(income.Id, income.Title, amountRecordCollection, income.ReferenceDate));
            }

            FinanceStatementCollection expenseCollection = new FinanceStatementCollection();
            foreach (var expense in expenses)
            {
                IEnumerable<Entities.AmountRecord> amountRecords = await _context
                    .AmountRecords
                    .Find(x => x.FinanceStatementId == expense.Id)
                    .ToListAsync();

                AmountRecordCollection amountRecordCollection = new AmountRecordCollection(amountRecords.Select(x => AmountRecord.Load(x.Id, x.Description, x.Amount)));

                expenseCollection.Add(Expense.Load(expense.Id, expense.Title, amountRecordCollection, expense.ReferenceDate));
            }

            FinanceStatementCollection investmentsCollection = new FinanceStatementCollection();
            foreach (var investment in investments)
            {
                IEnumerable<Entities.AmountRecord> amountRecords = await _context
                    .AmountRecords
                    .Find(x => x.FinanceStatementId == investment.Id)
                    .ToListAsync();

                AmountRecordCollection amountRecordCollection = new AmountRecordCollection(amountRecords.Select(x => AmountRecord.Load(x.Id, x.Description, x.Amount)));

                investmentsCollection.Add(Investment.Load(investment.Id, investment.Title, amountRecordCollection, investment.ReferenceDate));
            }

            AccountQueryResult result = new AccountQueryResult(account.Id, expenseCollection, incomeCollection, investmentsCollection);

            return result;
        }
    }
}
