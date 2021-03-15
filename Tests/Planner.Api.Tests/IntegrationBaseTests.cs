using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Planner.Infrastructure;
using System.Net.Http;

namespace Planner.Api.Tests
{
    public class IntegrationBaseTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public HttpClient Client
        {
            get
            {
                return _client;
            }
        }

        public IntegrationBaseTests()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.Development.json", optional: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddConfig(context.Configuration);
                    services.Register();
                });

            _server = new TestServer(webHostBuilder);
            _client = _server.CreateClient();
        }

    }
}
