using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using XamarinAzureChallenge.Shared.Models;

namespace Microsoft.XamarinAzureChallenge.AZF
{
    public static class SubmitChallengeFunction
    {
        private static Lazy<HttpClient> clientHolder = new Lazy<HttpClient>();
        private static readonly string apiHost = Environment.GetEnvironmentVariable("API_HOST");
        private static readonly string endPoint = Environment.GetEnvironmentVariable("END_POINT");
        private static readonly string uri = apiHost + endPoint;

        private static HttpClient Client => clientHolder.Value;

        [FunctionName(nameof(SubmitChallengeFunction))]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")][FromBody] User user)
        {
            var (isDataValid, errorMessage) = IsDataValid(user);

            if (!isDataValid)
            {
                return new BadRequestErrorMessageResult(errorMessage);
            }

            HttpResponseMessage result;
            try
            {
                result = await SendToApi(user).ConfigureAwait(false);
            }
            catch
            {
                return new InternalServerErrorResult();
            }

            switch (result.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return new BadRequestErrorMessageResult(result.ReasonPhrase);
                case HttpStatusCode.Conflict:
                    return new AspNetCore.Mvc.ConflictResult();
                case HttpStatusCode.OK:
                    return new OkResult();
                default:
                    return new InternalServerErrorResult();
            }
        }

        private static (bool isDataValid, string errorMessage) IsDataValid(User user)
        {
            if (user is null)
                return (false, "User Object Null");

            var stringBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(user.Phone))
                stringBuilder.AppendLine("Phone Number Null or Empty");
            if (string.IsNullOrWhiteSpace(user.Name))
                stringBuilder.AppendLine("Name Null or Empty");
            if (string.IsNullOrWhiteSpace(user.Email))
                stringBuilder.AppendLine("Email Null or Empty");

            if (stringBuilder.ToString().Length > 1)
                return (false, stringBuilder.ToString());

            return (true, "");
        }

        private static Task<HttpResponseMessage> SendToApi(User user)
        {
            var serializedUser = JsonConvert.SerializeObject(user);

            var httpContent = new StringContent(serializedUser, Encoding.UTF7, "application/json");

            return Client.PostAsync(uri, httpContent);
        }
    }
}
