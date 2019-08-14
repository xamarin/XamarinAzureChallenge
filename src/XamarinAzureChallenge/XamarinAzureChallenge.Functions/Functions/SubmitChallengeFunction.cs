using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using XamarinAzureChallenge.Shared.Models;

namespace Microsoft.XamarinAzureChallenge.AZF
{
    public static class SubmitChallengeFunction
    {
        private static readonly Lazy<HttpClient> clientHolder = new Lazy<HttpClient>();
        private static readonly string validationEndPoint = Environment.GetEnvironmentVariable("END_POINT");
        private static readonly string instanceId = Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID");

        private static HttpClient Client => clientHolder.Value;

        [FunctionName(nameof(SubmitChallengeFunction))]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")][FromBody] User user, ILogger log, ExecutionContext context)
        {
            log.LogInformation("HTTP Triggered");

            var (isDataValid, errorMessage) = IsDataValid(user);

            if (!isDataValid)
            {
                log.LogInformation($"Invalid Data: {errorMessage}");
                return new BadRequestErrorMessageResult(errorMessage);
            }

            HttpResponseMessage result;
            try
            {
                result = await SendToApi(user, context).ConfigureAwait(false);
            }
            catch
            {
                return new InternalServerErrorResult();
            }

            switch (result.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    log.LogInformation($"Bad request: {result.ReasonPhrase}");
                    return new BadRequestErrorMessageResult(result.ReasonPhrase);

                case HttpStatusCode.Conflict:
                    log.LogInformation("Error: Entrant Already Submitted");
                    return new AspNetCore.Mvc.ConflictResult();

                case HttpStatusCode.OK:
                    log.LogInformation("Success");
                    return new OkResult();

                default:
                    log.LogInformation("Unknown Error Ocurred");
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

        private static Task<HttpResponseMessage> SendToApi(User user, ExecutionContext context)
        {
            var serializedUser = JsonConvert.SerializeObject(user);

            var httpContent = new StringContent(serializedUser, Encoding.UTF7, "application/json");

            return Client.PostAsync($"{validationEndPoint}?invocationId={context.InvocationId}&instanceId={instanceId}", httpContent);
        }
    }
}
