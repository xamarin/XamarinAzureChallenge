using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinAzureChallenge.Pages;
using XamarinAzureChallenge.Shared.Models;

namespace XamarinAzureChallenge.ViewModels
{
    public class UserDataViewModel : BaseViewModel
    {
#error Missing Azure Function Endpoint Url. Replace "Enter Your Function API Url Here" with your Azure Function Endopint Url
        const string endpoint = "Enter Your Function API Url Here";
        readonly Lazy<HttpClient> clientHolder = new Lazy<HttpClient>();

        User user = new User();
        bool isBusy;

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

        HttpClient Client => clientHolder.Value;

        async Task SubmitCommmandExecute(User submittedUser)
        {        

            try
            {
                var areFieldsValid = await AreFieldsValid(submittedUser.Name, submittedUser.Email, submittedUser.Phone, submittedUser.IsTermsOfServiceAccepted);

                if (areFieldsValid)
                {
                    IsBusy = true;
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

        async Task<bool> AreFieldsValid(string name, string email, string phone, bool isTermsOfServiceAccepted)
        {
            var builder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(name))
                builder.AppendLine("Name cannot be blank");
            else if (!name.Trim().Contains(" "))
                builder.AppendLine("Full Name Required");
            
            if (string.IsNullOrWhiteSpace(email))
                builder.AppendLine("Email cannot be blank");

            if (string.IsNullOrWhiteSpace(phone))
                builder.AppendLine("Phone cannot be blank");

            if (!isTermsOfServiceAccepted)
                builder.AppendLine("Terms of Service Not Accepted");

            if (builder.Length != 0)
                await DisplayInvalidFieldAlert(builder.ToString());

            return builder.Length == 0;
        }

        Task PrivacyStatementCommandExecute() =>
            MainThread.InvokeOnMainThreadAsync(() => Browser.OpenAsync(new Uri("https://privacy.microsoft.com/privacystatement")));

        void OnSubmissionFailed(string message) => SubmissionFailed?.Invoke(this, message);
    }
}
