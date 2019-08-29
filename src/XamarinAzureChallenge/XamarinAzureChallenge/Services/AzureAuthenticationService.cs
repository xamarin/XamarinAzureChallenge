using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinAzureChallenge.Shared;
using XamarinAzureChallenge.ViewModels;

namespace XamarinAzureChallenge
{
    public static class AzureAuthenticationService
    {
        const string _oauthTokenKey = "OAuthToken";

        private static readonly Lazy<HttpClient> clientHolder = new Lazy<HttpClient>();
        private static readonly Lazy<JsonSerializer> serializer = new Lazy<JsonSerializer>();

        private static string _sessionAuthenticationId;

        public static event EventHandler AuthorizeSessionStarted;
        public static event EventHandler<AzureAuthenticationCompletedEventArgs> AuthenticationCompleted;

        private static HttpClient Client => clientHolder.Value;
        private static JsonSerializer Serializer => serializer.Value;

        public static async Task AuthorizeSession(Uri callbackUri)
        {
            OnAuthorizeSessionStarted();

            var code = HttpUtility.ParseQueryString(callbackUri.Query).Get("code");
            var state = HttpUtility.ParseQueryString(callbackUri.Query).Get("state");
            var errorDescription = HttpUtility.ParseQueryString(callbackUri.Query).Get("error_description");

            if (string.IsNullOrEmpty(code))
                errorDescription = "Invalid Authorization Code";

            if (state != _sessionAuthenticationId)
                errorDescription = "Invalid SessionId";

            if (string.IsNullOrEmpty(errorDescription))
            {
                _sessionAuthenticationId = string.Empty;

                var clientId = await GetAzureClientId();

                var content = new Dictionary<string,string>
                {
                    { "grant_type", "authorization_code" },
                    { "client_id", clientId },
                    { "code", code },
                    { "redirect_uri", $"{nameof(XamarinAzureChallenge).ToLower()}://auth" }
                };

                using (var response = await Client.PostAsync("https://login.microsoftonline.com/common/oauth2/token", new FormUrlEncodedContent(content)))
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var azureToken = JsonConvert.DeserializeObject<AzureToken>(responseContent);

                        await SaveAzureToken(azureToken);

                        OnAuthenticationCompleted(true);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Azure Authentication Unsuccessful", responseContent, "Ok");
                        OnAuthenticationCompleted(false);
                    }
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Azure Authentication Unsuccessful", errorDescription, "Ok");
                OnAuthenticationCompleted(false);
            }
        }

        public static async Task<AzureToken> GetAzureToken()
        {
            var serializedToken = await SecureStorage.GetAsync(_oauthTokenKey).ConfigureAwait(false);

            try
            {
                var token = JsonConvert.DeserializeObject<AzureToken>(serializedToken);

                if (token is null)
                    return new AzureToken();

                return token;
            }
            catch (ArgumentNullException)
            {
                return new AzureToken();
            }
            catch (JsonReaderException)
            {
                return new AzureToken();
            }
        }

        public static async Task OpenAuthenticationPage()
        {
            var azureClientId = await GetAzureClientId();

            _sessionAuthenticationId = Guid.NewGuid().ToString();

            var azureLoginUrl = $"https://login.microsoftonline.com/common/oauth2/authorize?client_id={azureClientId}&response_type=code&state={_sessionAuthenticationId}";

            await Device.InvokeOnMainThreadAsync(() => Browser.OpenAsync(azureLoginUrl));
        }

        static async Task<string> GetAzureClientId()
        {
            using (var stream = await Client.GetStreamAsync("https://xamarinazurechallenge-private.azurewebsites.net/api/GetClientId"))
            using (var streamReader = new StreamReader(stream))
            using (var json = new JsonTextReader(streamReader))
            {
                var azureClientIdModel = Serializer.Deserialize<AzureClientIdModel>(json);
                return azureClientIdModel.ClientId;
            }
        }

        private static async Task SaveAzureToken(AzureToken token)
        {
            if (token is null)
                throw new ArgumentNullException(nameof(token));

            if (token.AccessToken is null)
                throw new ArgumentNullException(nameof(token.AccessToken));

            var serializedToken = JsonConvert.SerializeObject(token);
            await SecureStorage.SetAsync(_oauthTokenKey, serializedToken);
        }

        private static void OnAuthorizeSessionStarted() => AuthorizeSessionStarted?.Invoke(null, EventArgs.Empty);

        private static void OnAuthenticationCompleted(bool isAuthenticationSuccessful) =>
            AuthenticationCompleted?.Invoke(null, new AzureAuthenticationCompletedEventArgs(isAuthenticationSuccessful));
    }
}
