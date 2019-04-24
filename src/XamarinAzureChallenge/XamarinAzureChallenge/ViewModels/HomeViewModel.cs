using System.Threading.Tasks;
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
            SourceCodeLabelTappedCommand = new Command(async () => await ExecuteSourceCodeLabelTappedCommand());
            TermsAndConditionsLabelTappedCommand = new Command(async () => await ExecuteTermsAndConditionsLabelTappedCommand());
        }

        public ICommand StartChallengeCommand { get; }
        public ICommand SourceCodeLabelTappedCommand { get; }
        public ICommand TermsAndConditionsLabelTappedCommand { get; }

        private Task ExecuteSourceCodeLabelTappedCommand() =>
            RunOnUIThread(async () => await Xamarin.Essentials.Browser.OpenAsync("https://github.com/xamarin/XamarinAzureChallenge"));

        private Task ExecuteTermsAndConditionsLabelTappedCommand() =>
            RunOnUIThread(async () => await Xamarin.Essentials.Browser.OpenAsync("https://github.com/xamarin/XamarinAzureChallenge/blob/master/TermsAndConditions.md"));
    }
}
