using Planner.Application.Commands.Register;
using System.Threading.Tasks;
using Xunit;

namespace Planner.UseCases.Tests
{
    public class RegisterUseCaseTests
    {
        private readonly IRegisterUseCase registerUseCase;

        public RegisterUseCaseTests()
        {
            registerUseCase = new RegisterUseCase(null, null, null);
        }

        [Fact]
        public async Task Should_Create_New_Account()
        {
            await registerUseCase.Execute(null);
        }
    }
}
