using XamarinAzureChallenge.ViewModels;

namespace XamarinAzureChallenge.Pages
{
    public partial class UserDataPage : BaseContentPage<UserDataViewModel>
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