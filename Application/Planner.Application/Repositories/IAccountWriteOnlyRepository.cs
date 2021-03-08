﻿using Planner.Domain.Accounts;
using System.Threading.Tasks;

namespace Planner.Application.Repositories
{
    public interface IAccountWriteOnlyRepository
    {
        Task Create(Account account);

        Task Update(Account account);

        Task Update(Account account, IFinanceStatement financeStatement);
        Task Update(Account account, AmountRecord financeStatement);

        Task Remove(Account account);

        Task Remove(Account account, IFinanceStatement financeStatement);
        Task Remove(Account account, AmountRecord financeStatement);
    }
}
