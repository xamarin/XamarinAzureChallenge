using System.Net.Http;
using Xamarin.Forms;
using XamarinAzureChallenge.ViewModels;

namespace XamarinAzureChallenge.Pages
{
    public partial class ResultPage : ContentPage
    {
        public ResultPage()
        {
            InitializeComponent();
        }

        public ResultPage(HttpResponseMessage response)
        {
            InitializeComponent();

            BindingContext = new ResultViewModel(response);
        }
    }
}