using Xamarin.Forms;
using XamarinAzureChallenge.ViewModels;

namespace XamarinAzureChallenge.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel();
        }
    }
}