using Planner.Api.UseCases.SignIn;
using Planner.Api.UseCases.SignUp;
using Planner.Application.Commands.SignIn;
using Planner.Application.Commands.SignUp;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Planner.Api.Tests
{
    public class UserTests : IntegrationBaseTests
    {
        const string mediaType = "application/json";

        [Fact]
        public async Task Should_SignUp_New_Account()
        {
            var signUpRequest = new SignUpRequest
            {
                Email = "ronaldinhogaucho@gmail.com",
                Name = "Ronaldo",
                Password = "r10bruxo",
                Picture = new Model.PictureModel
                {
                    Bytes = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    Name = "r10_profile",
                    Size = 5200,
                    Type = "jpeg"
                }
            };

            var result = await SignUp(signUpRequest);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_SignIn_Account()
        {
            var request = new SignInRequest
            {
                Email = "ronaldinhogaucho@gmail.com",
                Password = "r10bruxo",
            };

            var result = await SignIn(request);

            Assert.NotNull(result);
            Assert.Equal("Ronaldo", result.Name);
        }

        private async Task<SignInResult> SignIn(SignInRequest signInRequest)
        {
            string data = JsonSerializer.Serialize(signInRequest);
            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var response = await Client.PostAsync("api/User/SignIn", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<SignInResult>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }

        private async Task<SignUpResult> SignUp(SignUpRequest signUpRequest)
        {
            string data = JsonSerializer.Serialize(signUpRequest);
            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var response = await Client.PostAsync("api/User/SignUp", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<SignUpResult>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
    }
}
