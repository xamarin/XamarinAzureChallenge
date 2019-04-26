using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinAzureChallenge.Pages;
using XamarinAzureChallenge.Shared.Models;

namespace XamarinAzureChallenge.ViewModels
{
    public class UserDataViewModel : BaseViewModel
    {
#error Missing Azure Function Endpoint Url. Replace "Enter Your Function API Url Here" with your Azure Function Endopint Url
        private const string Endpoint = "Enter Your Function API Url Here";
        private readonly Lazy<HttpClient> clientHolder = new Lazy<HttpClient>();

        private User user;
        private bool isBusy;

        public UserDataViewModel()
        {
            User = new User();
            SubmitCommand = new Command(async () => await SubmitCommmandExecute(User));
            PrivacyStatementCommand = new Command(async () => await PrivacyStatementCommandExecute());
        }

        public ICommand SubmitCommand { get; }
        public ICommand PrivacyStatementCommand { get; }

        public bool IsBusy
        {
            get => isBusy;
            set => SetAndRaisePropertyChanged(ref isBusy, value);
        }

        public User User
        {
            get => user;
            set => SetAndRaisePropertyChanged(ref user, value);
        }

        private HttpClient Client => clientHolder.Value;

        private async Task SubmitCommmandExecute(User submittedUser)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var areFieldsValid = await AreFieldsValid(submittedUser.Name, submittedUser.Email, submittedUser.Phone, submittedUser.IsTermsOfServiceAccepted);

                if (areFieldsValid)
                {
                    var serializedUser = JsonConvert.SerializeObject(User);

                    var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");

                    var result = await Client.PostAsync(Endpoint, content);

                    await NavigateToPage(new ResultPage(result.StatusCode));
                }
            }
            catch
            {
                await NavigateToPage(new ResultPage(default));
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
            RunOnUIThread(async () => await Xamarin.Essentials.Browser.OpenAsync(new Uri("https://privacy.microsoft.com/privacystatement")));
    }
}
