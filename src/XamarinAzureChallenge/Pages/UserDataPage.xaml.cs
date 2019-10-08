using Xamarin.Forms;
using XamarinAzureChallenge.ViewModels;

namespace XamarinAzureChallenge.Pages
{
    public partial class UserDataPage : ContentPage
    {
        public UserDataPage()
        {
            InitializeComponent();

            var userDataViewModel = new UserDataViewModel();
            userDataViewModel.SubmissionFailed += HandleSubmissionFailed;

            BindingContext = userDataViewModel;
        }

        private void HandleSubmissionFailed(object sender, string message) => DisplayAlert("Submission Failed", message, "OK");
    }
}