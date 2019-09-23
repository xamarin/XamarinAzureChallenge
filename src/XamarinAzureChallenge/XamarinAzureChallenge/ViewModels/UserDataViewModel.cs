using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinAzureChallenge.Pages;
using XamarinAzureChallenge.Shared.Models;

namespace XamarinAzureChallenge.ViewModels
{
    public class UserDataViewModel : BaseViewModel
    {
#error Missing Azure Function Endpoint Url. Replace "Enter Your Function API Url Here" with your Azure Function Endopint Url
        private const string endpoint = "Enter Your Function API Url Here";
        private readonly Lazy<HttpClient> clientHolder = new Lazy<HttpClient>();

        private User user = new User();

        private bool isBusy;

        public UserDataViewModel()
        {
            SubmitCommand = new Command(async () => await SubmitCommmandExecute(User), () => !IsBusy);
            PrivacyStatementCommand = new Command(async () => await PrivacyStatementCommandExecute());
        }

        public event EventHandler<string> SubmissionFailed;

        public Command PrivacyStatementCommand { get; }
        public Command SubmitCommand { get; }

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

            IsBusy = true;

            try
            {
                var areFieldsValid = await AreFieldsValid(submittedUser.Name, submittedUser.Email, submittedUser.Phone, submittedUser.IsTermsOfServiceAccepted);

                if (areFieldsValid)
                {
                    var serializedUser = JsonConvert.SerializeObject(submittedUser);

                    using (var content = new StringContent(serializedUser, Encoding.UTF8, "application/json"))
                    using (var response = await Client.PostAsync(endpoint, content))
                    {
                        await NavigateToPage(new ResultPage(response));
                    }
                }
            }
            catch (Exception e)
            {
                OnSubmissionFailed(e.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<bool> AreFieldsValid(string name, string email, string phone, bool isTermsOfServiceAccepted)
        {
            var result = false;

            if (string.IsNullOrWhiteSpace(name))
            {
                await DisplayInvalidFieldAlert("Name cannot be blank");
            }
            else if (!name.Trim().Contains(" "))
            {
                await DisplayInvalidFieldAlert("Full Name Required");
            }
            else if (string.IsNullOrWhiteSpace(email))
            {
                await DisplayInvalidFieldAlert("Email cannot be blank");
            }
            else if (string.IsNullOrWhiteSpace(phone))
            {
                await DisplayInvalidFieldAlert("Phone cannot be blank");
            }
            else if (!isTermsOfServiceAccepted)
            {
                await DisplayInvalidFieldAlert("Terms of Service Not Accepted");
            }
            else
            {
                result = true;
            }

            return result;
        }

        private Task PrivacyStatementCommandExecute() =>
            Device.InvokeOnMainThreadAsync(() => Xamarin.Essentials.Browser.OpenAsync(new Uri("https://privacy.microsoft.com/privacystatement")));

        private void OnSubmissionFailed(string message) => SubmissionFailed?.Invoke(this, message);
    }
}
