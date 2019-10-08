using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinAzureChallenge.Pages;

namespace XamarinAzureChallenge.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            StartChallengeCommand = new Command(async () => await NavigateToPage(new UserDataPage()));
            SourceCodeLabelTappedCommand = new Command(async () => await ExecuteSourceCodeLabelTappedCommand());
            TermsAndConditionsLabelTappedCommand = new Command(async () => await ExecuteTermsAndConditionsLabelTappedCommand());
        }

        public ICommand StartChallengeCommand { get; }
        public ICommand SourceCodeLabelTappedCommand { get; }
        public ICommand TermsAndConditionsLabelTappedCommand { get; }

        Task ExecuteSourceCodeLabelTappedCommand() =>
            MainThread.InvokeOnMainThreadAsync(() => Browser.OpenAsync("https://github.com/xamarin/XamarinAzureChallenge"));
        
        Task ExecuteTermsAndConditionsLabelTappedCommand() =>
            MainThread.InvokeOnMainThreadAsync(() => Browser.OpenAsync("https://github.com/xamarin/XamarinAzureChallenge/blob/master/TermsAndConditions.md"));
    }
}
