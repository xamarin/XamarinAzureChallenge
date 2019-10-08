using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using XamarinAzureChallenge.Shared.Models;
using Microsoft.Azure.Services.AppAuthentication;

namespace XamarinAzureChallenge.Functions
{
    public static class ApiService
    {
        private static readonly string submissionEndpoint = Environment.GetEnvironmentVariable("END_POINT");
        private static readonly string instanceId = Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID");

        private static readonly Lazy<JsonSerializer> serializerHolder = new Lazy<JsonSerializer>();
        private static readonly Lazy<HttpClient> clientHolder = new Lazy<HttpClient>(() => new HttpClient(new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.GZip }));

        private static HttpClient Client => clientHolder.Value;
        private static JsonSerializer JsonSerializer => serializerHolder.Value;

        public static Task<HttpResponseMessage> SendChallengeSubmission(User user, Guid azureSubscription, ExecutionContext context)
        {
            var serializedUser = JsonConvert.SerializeObject(user);

            var httpContent = new StringContent(serializedUser);

            return Client.PostAsync($"{submissionEndpoint}/{context.InvocationId}/{instanceId}/{azureSubscription}", httpContent);
        }

        public static async Task<Guid> GetAzureSubscriptionGuid()
        {
            await AddAuthTokenToHeaders().ConfigureAwait(false);

            var azureSubscriptionResponse = await GetObjectFromApi<AzureSubscriptionResponse>("https://management.azure.com/subscriptions?api-version=2016-06-01").ConfigureAwait(false);

            if (azureSubscriptionResponse?.Subscriptions?.Any() != true)
                throw new Exception("No Azure Subscription Found. Ensure the Azure Function has been granted access to its Resource Group: https://docs.microsoft.com/azure/role-based-access-control/role-assignments-portal#overview-of-access-control-iam");

            return azureSubscriptionResponse.Subscriptions.First().SubscriptionId;
        }

        private static async Task AddAuthTokenToHeaders()
        {
            try
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var authenticationResult = await azureServiceTokenProvider.GetAuthenticationResultAsync("https://management.azure.com/").ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(authenticationResult?.AccessToken))
                    throw new Exception("Invalid Access Token. Ensure the Azure Function has a system-assigned Identity: https://docs.microsoft.com/azure/app-service/overview-managed-identity#adding-a-system-assigned-identity");

                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authenticationResult.TokenType, authenticationResult.AccessToken);
            }
            catch (AzureServiceTokenProviderException)
            {
                throw new Exception("Error: Function must be running on Azure");
            }
        }

        private static async Task<T> GetObjectFromApi<T>(string url)
        {
            using (var stream = await Client.GetStreamAsync(url).ConfigureAwait(false))
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return JsonSerializer.Deserialize<T>(jsonReader);
            }
        }

        class AzureSubscriptionResponse
        {
            [JsonProperty("value")]
            public List<AzureSubscription> Subscriptions { get; set; }
        }

        class AzureSubscription
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("subscriptionId")]
            public Guid SubscriptionId { get; set; }

            [JsonProperty("displayName")]
            public string DisplayName { get; set; }
        }
    }
}
