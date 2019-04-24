using System.Windows.Input;
using Xamarin.Forms;
using XamarinAzureChallenge.Pages;

namespace XamarinAzureChallenge.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            StartChallengeCommand = new Command(async () => await NavigateToPage(new UserDataPage()));
        }

        public ICommand StartChallengeCommand { get; }
    }
}
