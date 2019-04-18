using System.Net;
using XamarinAzureChallenge.ViewModels;

namespace XamarinAzureChallenge.Pages
{
    public partial class ResultPage : BaseContentPage<ResultViewModel>
	{
        public ResultPage(HttpStatusCode statusCode)
        {
            InitializeComponent();

            BindingContext = new ResultViewModel(statusCode);

            BackButton.IsVisible = statusCode != HttpStatusCode.OK;
        }
    }
}