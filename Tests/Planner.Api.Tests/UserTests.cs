using Planner.Api.Model;
using Planner.Api.UseCases.SignIn;
using Planner.Api.UseCases.SignUp;
using Planner.Application.Commands.CreateFinanceStatement;
using Planner.Application.Commands.RemoveFinanceStatement;
using Planner.Application.Commands.SaveAmountRecord;
using Planner.Application.Commands.SignIn;
using Planner.Application.Commands.SignUp;
using System;
using System.Net;
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

        JsonSerializerOptions jsonSerializerOptions = new(JsonSerializerDefaults.Web);

        [Fact]
        public async Task Full_Test()
        {
            Guid accountId;
            Guid userId;
            Guid expenseToRemove;
            Guid incomeToRemove;
            Guid investmentToRemove;

            var signUpRequest = new SignUpRequest
            {
                Email = "ronaldoFenomeno@gmail.com",
                Name = "Ronaldo",
                Password = "fenomeno9",
                Picture = new Model.PictureModel
                {
                    Bytes = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    Name = "r9_profile",
                    Size = 5200,
                    Type = "jpeg"
                }
            };

            await SignUp(signUpRequest);

            var signInRequest = new SignInRequest
            {
                Email = "ronaldoFenomeno@gmail.com",
                Password = "fenomeno9"
            };

            var signInResult = await SignIn(signInRequest);


            accountId = signInResult.AccountId;
            userId = signInResult.UserId;

            AddToken(signInResult.Token);

            await GetAccount(accountId);

            var createExpenseRequest = new CreateFinanceStatementRequest
            {
                AccountId = accountId,
                Title = "Educação",
                Amount = 500,
            };


            var createExpense2Request = new CreateFinanceStatementRequest
            {
                AccountId = accountId,
                Title = "Internet",
                Amount = 100
            };

            await CreateExpense(createExpenseRequest);
            expenseToRemove = await CreateExpense(createExpense2Request);


            var createIncomeRequest = new CreateFinanceStatementRequest
            {
                AccountId = accountId,
                Title = "bonus",
                Amount = 2000,
            };

            var createIncome2Request = new CreateFinanceStatementRequest
            {
                AccountId = accountId,
                Title = "aluguel",
                Amount = 130,
            };

            await CreateIncome(createIncomeRequest);
            incomeToRemove = await CreateIncome(createIncome2Request);

            var createInvestmentRequest = new CreateFinanceStatementRequest
            {
                AccountId = accountId,
                Title = "Fundo de emergência",
                Amount = 300,
            };

            var createInvestment2Request = new CreateFinanceStatementRequest
            {
                AccountId = accountId,
                Title = "Ações",
                Amount = 1000,
            };

            await CreateInvestment(createInvestmentRequest);
            investmentToRemove = await CreateInvestment(createInvestment2Request);

            await SaveExpenseAmountRecord(accountId, expenseToRemove);
            await SaveIncomeAmountRecord(accountId, incomeToRemove);
            await SaveInvestmentAmountRecord(accountId, investmentToRemove);


            await UpdateExpense(accountId, expenseToRemove, "new expense title");
            await UpdateIncome(accountId, incomeToRemove, "new income title");
            await UpdateInvestment(accountId, investmentToRemove, "new investment title");

            await RemoveExpense(accountId, expenseToRemove);
            await RemoveIncome(accountId, incomeToRemove);
            await RemoveInvestment(accountId, investmentToRemove);

            await DeleteAccount(userId);
        }

        private async Task DeleteAccount(Guid userId)
        {
            var response = await Client.DeleteAsync($"api/User/{userId}");

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task RemoveInvestment(Guid accountId, Guid investmentToRemove)
        {
            var removeInvestmentRequest = new RemoveFinanceStatementRequest
            {
                AccountId = accountId,
                Id = investmentToRemove
            };

            string data = JsonSerializer.Serialize(removeInvestmentRequest);
            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var request = new HttpRequestMessage(HttpMethod.Delete, "api/Investment")
            {
                Content = content
            };

            var response = await Client.SendAsync(request);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<RemoveFinanceStatementResult>(responseString, jsonSerializerOptions);

            Assert.NotNull(result);
        }

        private async Task RemoveIncome(Guid accountId, Guid incomeToRemove)
        {
            var removeIncomeRequest = new RemoveFinanceStatementRequest
            {
                AccountId = accountId,
                Id = incomeToRemove
            };

            string data = JsonSerializer.Serialize(removeIncomeRequest);
            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var request = new HttpRequestMessage(HttpMethod.Delete, "api/Income")
            {
                Content = content
            };

            var response = await Client.SendAsync(request);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<RemoveFinanceStatementResult>(responseString, jsonSerializerOptions);

            Assert.NotNull(result);
        }

        private async Task RemoveExpense(Guid accountId, Guid expenseToRemove)
        {
            var removeExpenseRequest = new RemoveFinanceStatementRequest
            {
                AccountId = accountId,
                Id = expenseToRemove
            };

            string data = JsonSerializer.Serialize(removeExpenseRequest);
            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var request = new HttpRequestMessage(HttpMethod.Delete, "api/Expense")
            {
                Content = content
            };
            var response = await Client.SendAsync(request);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<RemoveFinanceStatementResult>(responseString, jsonSerializerOptions);

            Assert.NotNull(result);
        }

        private async Task UpdateInvestment(Guid accountId, Guid investmentId, string title)
        {
            var request = new UpdateFinanceStatementRequest
            {
                AccountId = accountId,
                Id = investmentId,
                Title = title
            };

            string data = JsonSerializer.Serialize(request);
            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var response = await Client.PutAsync("api/Investment", content);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        private async Task UpdateIncome(Guid accountId, Guid incomeId, string title)
        {
            var request = new UpdateFinanceStatementRequest
            {
                AccountId = accountId,
                Id = incomeId,
                Title = title
            };

            string data = JsonSerializer.Serialize(request);
            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var response = await Client.PutAsync("api/Income", content);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        private async Task UpdateExpense(Guid accountId, Guid expenseId, string title)
        {
            var request = new UpdateFinanceStatementRequest
            {
                AccountId = accountId,
                Id = expenseId,
                Title = title
            };

            string data = JsonSerializer.Serialize(request);
            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var response = await Client.PutAsync("api/Expense", content);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        }

        private async Task SaveExpenseAmountRecord(Guid accountId, Guid expenseId)
        {
            var request = new SaveAmountRecordRequest
            {
                AccountId = accountId,
                Id = expenseId,
                AmountRecords = new Model.AmountRecordModel[] { new Model.AmountRecordModel { Description = "expense teste", Amount = 500 } }
            };

            string data = JsonSerializer.Serialize(request);
            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var response = await Client.PatchAsync("api/Expense", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<SaveAmountRecordResult>(responseString, jsonSerializerOptions);

            Assert.NotNull(result);
        }

        private async Task SaveIncomeAmountRecord(Guid accountId, Guid incomeId)
        {
            var request = new SaveAmountRecordRequest
            {
                AccountId = accountId,
                Id = incomeId,
                AmountRecords = new Model.AmountRecordModel[] { new Model.AmountRecordModel { Description = "income teste", Amount = 700 } }
            };

            string data = JsonSerializer.Serialize(request);
            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var response = await Client.PatchAsync("api/Income", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<SaveAmountRecordResult>(responseString, jsonSerializerOptions);

            Assert.NotNull(result);
        }

        private async Task SaveInvestmentAmountRecord(Guid accountId, Guid investmentId)
        {
            var request = new SaveAmountRecordRequest
            {
                AccountId = accountId,
                Id = investmentId,
                AmountRecords = new Model.AmountRecordModel[] { new Model.AmountRecordModel { Description = "investment teste", Amount = 300 } }
            };

            string data = JsonSerializer.Serialize(request);
            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var response = await Client.PatchAsync("api/Investment", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<SaveAmountRecordResult>(responseString, jsonSerializerOptions);

            Assert.NotNull(result);
        }

        private async Task<Guid> CreateIncome(CreateFinanceStatementRequest createIncomeRequest)
        {
            string data = JsonSerializer.Serialize(createIncomeRequest);

            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var response = await Client.PostAsync("api/Income", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CreateFinanceStatementResult>(responseString, jsonSerializerOptions);

            Assert.NotNull(result);

            return result.Id;
        }

        private async Task<Guid> CreateInvestment(CreateFinanceStatementRequest createInvestmentRequest)
        {
            string data = JsonSerializer.Serialize(createInvestmentRequest);

            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var response = await Client.PostAsync("api/Investment", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CreateFinanceStatementResult>(responseString, jsonSerializerOptions);

            Assert.NotNull(result);

            return result.Id;
        }

        private async Task GetAccount(Guid accountId)
        {
            var response = await Client.GetAsync($"api/Account/{accountId}");

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotNull(responseString);
            Assert.True(response.IsSuccessStatusCode);
        }

        private async Task<Guid> CreateExpense(CreateFinanceStatementRequest createExpenseRequest)
        {
            string data = JsonSerializer.Serialize(createExpenseRequest);

            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var response = await Client.PostAsync("api/Expense", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CreateFinanceStatementResult>(responseString, jsonSerializerOptions);

            Assert.NotNull(result);

            return result.Id;
        }


        private async Task<SignInResult> SignIn(SignInRequest signInRequest)
        {
            string data = JsonSerializer.Serialize(signInRequest);
            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var response = await Client.PostAsync("api/User/SignIn", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<SignInResult>(responseString, jsonSerializerOptions);

            return result;
        }

        private async Task SignUp(SignUpRequest signUpRequest)
        {
            string data = JsonSerializer.Serialize(signUpRequest);
            StringContent content = new StringContent(data, Encoding.UTF8, mediaType);

            var response = await Client.PostAsync("api/User/SignUp", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<SignUpResult>(responseString, jsonSerializerOptions);

            Assert.NotNull(result);
        }
    }
}
