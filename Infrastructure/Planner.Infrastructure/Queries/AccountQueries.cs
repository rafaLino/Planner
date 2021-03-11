using MongoDB.Driver;
using Planner.Application.Queries;
using Planner.Application.Results;
using Planner.Infrastructure.Exceptions;
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

        public async Task<AccountQueryResult> GetAccount(string accountId)
        {
            Entities.Account account = await _context
                .Accounts
                .Find(x => x.Id == accountId)
                .SingleOrDefaultAsync();

            if (account == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists");

            if (!account.Actived)
                throw new AccountDeactivatedException($"The account {accountId} is deactivated!");


            return null;
        }
    }
}
