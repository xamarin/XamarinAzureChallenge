using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinAzureChallenge.ViewModels
{
    public class ResultViewModel : BaseViewModel
    {
        private string textDetailResult, imageResult, textResult;
        private bool isBackButtonVisible;

        public ResultViewModel(HttpResponseMessage responseMessage)
        {
            EditYourSubmissionCommand = new Command(async () => await EditYourSubmissionCommandExecute());

            HandleHttpResponseMessage(responseMessage);
        }

        public ICommand EditYourSubmissionCommand { get; }

        public string ImageResult
        {
            get => imageResult;
            set => SetAndRaisePropertyChanged(ref imageResult, value);
        }

        public string TextResult
        {
            get => textResult;
            set => SetAndRaisePropertyChanged(ref textResult, value);
        }

        public bool IsBackButtonVisible
        {
            get => isBackButtonVisible;
            set => SetAndRaisePropertyChanged(ref isBackButtonVisible, value);
        }

        public string TextDetailResult
        {
            get => textDetailResult;
            set => SetAndRaisePropertyChanged(ref textDetailResult, value);
        }

        private Task EditYourSubmissionCommandExecute() => NavigateBack();

        async void HandleHttpResponseMessage(HttpResponseMessage responseMessage)
        {
            IsBackButtonVisible = !responseMessage.IsSuccessStatusCode;

            if (responseMessage.IsSuccessStatusCode)
            {
                ImageResult = "resultOk";
                TextResult = "Congratulations!";
                TextDetailResult = "You have successfully submited the \n Challenge Form!";
            }
            else
            {
                ImageResult = "resultFailed";
                TextResult = "Oops!";
                TextDetailResult = await responseMessage.Content.ReadAsStringAsync();
            }
        }
    }
}
