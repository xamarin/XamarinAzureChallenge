using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinAzureChallenge.Pages;
using XamarinAzureChallenge.Shared;
using XamarinAzureChallenge.Shared.Models;

namespace XamarinAzureChallenge.ViewModels
{
    public class UserDataViewModel : BaseViewModel
    {
        //#error Missing Azure Function Endpoint Url. Replace "Enter Your Function API Url Here" with your Azure Function Endopint Url
        private const string endpoint = "Enter Your Function API Url Here";
        private readonly Lazy<HttpClient> clientHolder = new Lazy<HttpClient>();

        private User user;
        private bool isBusy;

        public UserDataViewModel()
        {
            User = GetSavedUser();

            SubmitCommand = new Command(async () => await SubmitCommmandExecute(User), () => !IsBusy);
            PrivacyStatementCommand = new Command(async () => await PrivacyStatementCommandExecute());

            AzureAuthenticationService.AuthorizeSessionStarted += HandleAuthorizeSessionStarted;
        }

        public Command SubmitCommand { get; }
        public Command PrivacyStatementCommand { get; }

        public bool IsBusy
        {
            get => isBusy;
            set => SetAndRaisePropertyChanged(ref isBusy, value, () => Device.BeginInvokeOnMainThread(SubmitCommand.ChangeCanExecute));
        }

        public User User
        {
            get => user;
            set => SetAndRaisePropertyChanged(ref user, value);
        }

        private HttpClient Client => clientHolder.Value;

        private async Task SubmitCommmandExecute(User submittedUser)
        {
            var areFieldsValid = await AreFieldsValid(submittedUser.Name, submittedUser.Email, submittedUser.Phone, submittedUser.IsTermsOfServiceAccepted);

            if (areFieldsValid)
            {
                IsBusy = true;

                SaveUser(User);

                await AzureAuthenticationService.OpenAuthenticationPage();
            }
        }

        private void HandleAuthorizeSessionStarted(object sender, EventArgs e)
        {
            //Ensure the SubmitButton remains disabled until Authorization has completed
            IsBusy = true;

            // Listen for AuthenticationCompleted event
            AzureAuthenticationService.AuthenticationCompleted += HandleAuthenticationCompleted;

            // Always unsubscribe events to avoid memory leaks
            AzureAuthenticationService.AuthorizeSessionStarted -= HandleAuthorizeSessionStarted;
        }

        private async void HandleAuthenticationCompleted(object sender, AzureAuthenticationCompletedEventArgs e)
        {
            // Always unsubscribe events to avoid memory leaks
            AzureAuthenticationService.AuthenticationCompleted -= HandleAuthenticationCompleted;

            if (e.IsAuthenticationSuccessful)
            {
                var azureToken = await AzureAuthenticationService.GetAzureToken();
                var subscriptionId = await GetAzureSubscriptionId(azureToken);

                await SubmitUserInfo(User, subscriptionId);
            }

            IsBusy = false;
        }


        private async Task<string> GetAzureSubscriptionId(AzureToken azureToken)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(azureToken.TokenType, azureToken.AccessToken);

            using (var response = await Client.GetAsync("https://management.azure.com/subscriptions?api-version=2016-06-01"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Get Azure Subscription Id Failed", "", "Ok");
                    return string.Empty;
                }
            }
        }

        private async Task SubmitUserInfo(User submittedUser, string azureSubscriptionId)
        {
            try
            {
                var serializedUser = JsonConvert.SerializeObject(submittedUser);

                using (var content = new StringContent(serializedUser, Encoding.UTF8, "application/json"))
                using (var response = await Client.PostAsync($"{endpoint}/{azureSubscriptionId}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        await NavigateToPage(new ResultPage(response.StatusCode));
                    }
                    else
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        await Application.Current.MainPage.DisplayAlert("Submission Unsuccessful", responseContent, "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Submission Unsuccessful", ex.Message, "Ok");
            }
        }

        private async Task<bool> AreFieldsValid(string name, string email, string phone, bool isTermsOfServiceAccepted, bool shouldDisplayAlert = true)
        {
            var areFieldsValid = false;
            var errorMessage = "";

            if (string.IsNullOrWhiteSpace(name))
            {
                errorMessage = "Name cannot be blank";
            }
            else if (string.IsNullOrWhiteSpace(email))
            {
                errorMessage = "Email cannot be blank";
            }
            else if (string.IsNullOrWhiteSpace(phone))
            {
                errorMessage = "Phone cannot be blank";
            }
            else if (!isTermsOfServiceAccepted)
            {
                errorMessage = "Terms of Service Not Accepted";
            }
            else
            {
                areFieldsValid = true;
            }

            if (!areFieldsValid && shouldDisplayAlert)
                await DisplayInvalidFieldAlert(errorMessage);

            return areFieldsValid;
        }

        private User GetSavedUser()
        {
            var serializedUser = Preferences.Get(nameof(User), string.Empty);

            try
            {
                var token = JsonConvert.DeserializeObject<User>(serializedUser);

                if (token is null)
                    return new User();

                return token;
            }
            catch (ArgumentNullException)
            {
                return new User();
            }
            catch (JsonReaderException)
            {
                return new User();
            }
        }

        private void SaveUser(User currentUser)
        {
            if (currentUser is null)
                throw new ArgumentNullException(nameof(currentUser));

            var serializedUser = JsonConvert.SerializeObject(currentUser);
            Preferences.Set(nameof(User), serializedUser);
        }

        private Task PrivacyStatementCommandExecute() =>
            Device.InvokeOnMainThreadAsync(() => Xamarin.Essentials.Browser.OpenAsync(new Uri("https://privacy.microsoft.com/privacystatement")));
    }
}
