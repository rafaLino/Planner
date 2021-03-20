using Planner.Application.Repositories;
using System;
using System.Threading.Tasks;

namespace Planner.Application.Commands.DeleteAccount
{
    public class DeleteAccountUseCase : IDeleteAccountUseCase
    {
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;

        public DeleteAccountUseCase(IUserWriteOnlyRepository userWriteOnlyRepository)
        {
            _userWriteOnlyRepository = userWriteOnlyRepository;
        }

        public async Task Execute(Guid userId)
        {
            await _userWriteOnlyRepository.Remove(userId);
        }
    }
}
