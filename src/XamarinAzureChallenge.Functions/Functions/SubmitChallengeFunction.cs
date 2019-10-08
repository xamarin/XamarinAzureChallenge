using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using XamarinAzureChallenge.Functions;
using XamarinAzureChallenge.Shared.Models;

namespace Microsoft.XamarinAzureChallenge.Functions
{
    public static class SubmitChallengeFunction
    {
        [FunctionName(nameof(SubmitChallengeFunction))]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")][FromBody] User user, ILogger log, ExecutionContext context)
        {
            log.LogInformation("HTTP Function Triggered");

            var (isDataValid, errorMessage) = IsDataValid(user);

            if (!isDataValid)
            {
                log.LogInformation($"Invalid Data: {errorMessage}");
                return new BadRequestErrorMessageResult(errorMessage);
            }

            HttpResponseMessage response;

            try
            {
                var azureSubscription = await ApiService.GetAzureSubscriptionGuid().ConfigureAwait(false);
                response = await ApiService.SendChallengeSubmission(user, azureSubscription, context).ConfigureAwait(false);
            }
            catch(Exception e)
            {
                return new ObjectResult(e.Message) { StatusCode = StatusCodes.Status500InternalServerError };
            }

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    log.LogInformation($"Bad request: {response.ReasonPhrase}");
                    return new BadRequestErrorMessageResult(responseContent);

                case HttpStatusCode.Conflict:
                    log.LogInformation("Error: Entrant Already Submitted");
                    return new ConflictObjectResult(responseContent);

                case HttpStatusCode.Created:
                    log.LogInformation("Success");
                    return new ObjectResult(responseContent) { StatusCode = StatusCodes.Status201Created };

                default:
                    log.LogInformation("Unknown Error Ocurred");
                    return new ObjectResult(responseContent) { StatusCode = StatusCodes.Status500InternalServerError };
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

            if (stringBuilder.Length > 1)
            {
                //Remove the carriage return from AppdendLine
                stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);

                return (false, stringBuilder.ToString());
            }

            return (true, "");
        }
    }
}
