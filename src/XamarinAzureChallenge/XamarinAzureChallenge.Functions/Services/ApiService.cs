using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using XamarinAzureChallenge.Shared.Models;

namespace XamarinAzureChallenge.Functions
{
    public static class ApiService
    {
        private static readonly string submissionEndpoint = Environment.GetEnvironmentVariable("END_POINT");
        private static readonly string instanceId = Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID");
        private static readonly Lazy<HttpClient> clientHolder = new Lazy<HttpClient>(() => new HttpClient(new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.GZip }));

        private static HttpClient Client => clientHolder.Value;

        public static Task<HttpResponseMessage> SendChallengeSubmission(User user, ExecutionContext context)
        {
            var serializedUser = JsonConvert.SerializeObject(user);

            var httpContent = new StringContent(serializedUser);

            return Client.PostAsync($"{submissionEndpoint}/{context.InvocationId}/{instanceId}", httpContent);
        }

        public static async Task GetSubscriptionList(ILogger log)
        {
            await AddAuthTokenToHeaders(log).ConfigureAwait(false);

            var subscriptionResponse = await Client.GetAsync("https://management.azure.com/subscriptions?api-version=2016-06-01").ConfigureAwait(false);

            log.LogInformation($"Repsonse Status Code: {subscriptionResponse.StatusCode}");

            var subscriptionContent = await subscriptionResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            log.LogInformation($"Subscriptions Content: \n{subscriptionContent}");
        }

        static async Task AddAuthTokenToHeaders(ILogger log)
        {
            if (!string.IsNullOrWhiteSpace(Client.DefaultRequestHeaders.Authorization?.Parameter))
                return;

            var azureServiceTokenProvider = new AzureServiceTokenProvider("RunAs=App");
            var authenticationResult = await azureServiceTokenProvider.GetAuthenticationResultAsync("https://management.azure.com/subscriptions");

            log.LogInformation($"{nameof(authenticationResult.TokenType)}: {authenticationResult.TokenType}");
            log.LogInformation($"{nameof(authenticationResult.AccessToken)}: {authenticationResult.AccessToken}");

            if (string.IsNullOrWhiteSpace(authenticationResult?.AccessToken))
                throw new Exception("Invalid Access Token. Ensure the Azure Function has a system-assigned Identity: https://docs.microsoft.com/azure/app-service/overview-managed-identity#adding-a-system-assigned-identity");

            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authenticationResult.TokenType, authenticationResult.AccessToken);
        }
    }
}
