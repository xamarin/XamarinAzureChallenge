using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinAzureChallenge.Pages;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinAzureChallenge
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
			MainPage = new NavigationPage(new HomePage())
            {
                BarBackgroundColor = Color.FromHex("#3498db"),
                BarTextColor = Color.White
            };
        }
    }
}
