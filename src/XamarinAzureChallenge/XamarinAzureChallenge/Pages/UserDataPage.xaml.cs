using XamarinAzureChallenge.ViewModels;

namespace XamarinAzureChallenge.Pages
{
    public partial class UserDataPage : BaseContentPage<UserDataViewModel>
    {
        public UserDataPage()
        {
            InitializeComponent();

            BindingContext = new UserDataViewModel();
        }

        protected override void OnAppearing()
        {
            ViewModel.IsBusy = false;

            base.OnAppearing();
        }
    }
}