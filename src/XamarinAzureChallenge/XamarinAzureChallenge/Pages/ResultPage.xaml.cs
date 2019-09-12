using System.Net.Http;
using XamarinAzureChallenge.ViewModels;

namespace XamarinAzureChallenge.Pages
{
    public partial class ResultPage : BaseContentPage<ResultViewModel>
	{
        public ResultPage(HttpResponseMessage response)
        {
            InitializeComponent();

            BindingContext = new ResultViewModel(response);
        }
    }
}