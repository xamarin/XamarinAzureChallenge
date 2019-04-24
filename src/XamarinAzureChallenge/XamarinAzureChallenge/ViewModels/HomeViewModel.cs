using System.Windows.Input;
using Xamarin.Forms;
using XamarinAzureChallenge.Pages;

namespace XamarinAzureChallenge.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ICommand StartChallengeCommand { get; }

        public HomeViewModel()
        {
            StartChallengeCommand = new Command(async () => await NavigateToAsync(new UserDataPage()));
        }
    }
}
