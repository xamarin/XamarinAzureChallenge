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
        private const string Endpoint = "https://xamarinazfunction-dev.azurewebsites.net/api/MicrosoftXamarinChallengeAZF?code=DnbhWVUs5pM52HS886iw2m0RwPCUGwBBCpf8yHJWFgrpVhMchgO0/Q==";

        private HttpClient client;

        private HttpClient Client
        {
            get
            {
                if (client == null)
                {
                    client = new HttpClient();
                }

                return client;
            }
        }

        public UserDataViewModel()
        {
            this.User = new User();
        }

        private User user;
        public User User
        {
            get => user;
            set
            {
                SetAndRaisePropertyChanged(ref user, value);
            }
        }

        public ICommand SubmitCommand => new Command(async () => await SubmitCommmandExecute());

        private async Task SubmitCommmandExecute()
        {
            if (IsBusy) return;
            IsBusy = true;


            if (await ValidateFields())
            {
                var serializedUser = JsonConvert.SerializeObject(this.User);

                var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");

                var result = await this.Client.PostAsync(Endpoint, content);

                await NavigateToAsync(new ResultPage(result.StatusCode));
            }

            IsBusy = false;
        }

        private async Task<bool> ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(User.Email) || string.IsNullOrWhiteSpace(User.Name) || string.IsNullOrWhiteSpace(User.Phone))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "INVALID FIELDS",
                    "",
                    "Ok");
                return false;
            }

            if (!User.AcceptTermsOfService)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "INVALID FIELDS",
                    "You must accepts the term os service",
                    "Ok");
                return false;
            }

            return true;
        }

        public ICommand PrivacyStatementCommand => new Command(PrivacyStatementCommandExecute);

        private void PrivacyStatementCommandExecute()
        {
            Device.OpenUri(new Uri("https://privacy.microsoft.com/privacystatement"));
        }
    }
}
