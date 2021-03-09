using MongoDB.Driver;
using Planner.Application.Repositories;
using Planner.Domain.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planner.Infrastructure.Repositories
{
    public class AccountRepository : IAccountReadOnlyRepository, IAccountWriteOnlyRepository
    {
        private readonly Context _context;

        public AccountRepository(Context context)
        {
            _context = context;
        }


        public async Task<Account> Get(string accountId)
        {
            Entities.Account account = await _context
                .Accounts
                .Find(x => x.Id == accountId)
                .SingleOrDefaultAsync();


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

                expenseCollection.Add(Income.Load(expense.Id, expense.Title, amountRecordCollection, expense.ReferenceDate));
            }

            FinanceStatementCollection investmentsCollection = new FinanceStatementCollection();
            foreach (var investment in investments)
            {
                IEnumerable<Entities.AmountRecord> amountRecords = await _context
                    .AmountRecords
                    .Find(x => x.FinanceStatementId == investment.Id)
                    .ToListAsync();

                AmountRecordCollection amountRecordCollection = new AmountRecordCollection(amountRecords.Select(x => AmountRecord.Load(x.Id, x.Description, x.Amount)));

                investmentsCollection.Add(Income.Load(investment.Id, investment.Title, amountRecordCollection, investment.ReferenceDate));
            }

            Account result = Account.Load(
                account.Id,
                account.Actived,
                incomeCollection,
                expenseCollection,
                investmentsCollection
                );

            return result;
        }

        public async Task Create(Account account)
        {
            Entities.Account accountEntity = new Entities.Account
            {
                Actived = account.Actived
            };

            await _context.Accounts.InsertOneAsync(accountEntity);

            account.UpdateId(accountEntity.Id);
        }

        public async Task Remove(Account account)
        {

            foreach (var item in account.GetAll())
            {
                await _context.AmountRecords.DeleteManyAsync(x => x.FinanceStatementId == item.Id);
            }
            await _context.Expenses.DeleteManyAsync(x => x.AccountId == account.Id);
            await _context.Incomes.DeleteManyAsync(x => x.AccountId == account.Id);
            await _context.Investments.DeleteManyAsync(x => x.AccountId == account.Id);
            await _context.Accounts.DeleteOneAsync(x => x.Id == account.Id);
        }

        public async Task Remove(Account account, IFinanceStatement financeStatement)
        {
            await _context.AmountRecords.DeleteManyAsync(x => x.FinanceStatementId == financeStatement.Id);

            if (financeStatement is Expense)
                await _context.Expenses.DeleteOneAsync(x => x.Id == financeStatement.Id && x.AccountId == account.Id);

            else if (financeStatement is Income)
                await _context.Incomes.DeleteOneAsync(x => x.Id == financeStatement.Id && x.AccountId == account.Id);

            else if (financeStatement is Investment)
                await _context.Investments.DeleteOneAsync(x => x.Id == financeStatement.Id && x.AccountId == account.Id);
        }

        public async Task Remove(IFinanceStatement financeStatement, AmountRecord amountRecord)
        {
            await _context.AmountRecords.DeleteOneAsync(x => x.Id == amountRecord.Id && x.FinanceStatementId == financeStatement.Id);
        }

        public async Task Update(Account account, IFinanceStatement financeStatement)
        {
            Entities.FinanceStatement financeStatementEntity = new Entities.FinanceStatement
            {
                Id = financeStatement.Id,
                Title = financeStatement.Title,
                ReferenceDate = financeStatement.ReferenceDate,
                AccountId = account.Id,
            };

            IEnumerable<Entities.AmountRecord> amountRecordEntities = financeStatement
                .AmountRecords
                .GetAmountRecords()
                .Select(amountRecord => new Entities.AmountRecord
                {
                    Id = amountRecord.Id,
                    Amount = amountRecord.Amount,
                    Description = amountRecord.Description,
                    FinanceStatementId = financeStatement.Id
                });

            await _context.AmountRecords.InsertManyAsync(amountRecordEntities);

            if (financeStatement is Income)
                await _context.Incomes.InsertOneAsync(financeStatementEntity);

            if (financeStatement is Expense)
                await _context.Expenses.InsertOneAsync(financeStatementEntity);

            if (financeStatement is Investment)
                await _context.Investments.InsertOneAsync(financeStatementEntity);

            ((FinanceStatement)financeStatement).UpdateId(financeStatementEntity.Id);

        }

        public async Task Update(Account account, IFinanceStatement financeStatement, AmountRecord amountRecord)
        {
            Entities.AmountRecord amountRecordEntity = new Entities.AmountRecord
            {
                Id = amountRecord.Id,
                Amount = amountRecord.Amount,
                Description = amountRecord.Description,
                FinanceStatementId = financeStatement.Id
            };

            await _context.AmountRecords.InsertOneAsync(amountRecordEntity);

            amountRecord.UpdateId(amountRecordEntity.Id);
        }
    }
}
