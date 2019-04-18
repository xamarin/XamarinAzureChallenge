using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinAzureChallenge.ViewModels
{
    public class ResultViewModel : BaseViewModel
    {
        private string textDetailResult;
        private HttpStatusCode statusCode;

        public ResultViewModel(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;

            if (this.statusCode == HttpStatusCode.OK)
            {
                ImageResult = "resultOk";
                TextResult = "Congratulations!";
                TextDetailResult = "You have successfully submited the \n Challenge Form!";
            }
            else
            {
                ImageResult = "resultFailed";
                TextResult = "Opps!";
                if (this.statusCode == HttpStatusCode.BadRequest)
                {
                    TextDetailResult = "We detected duplicated data.\n Please go back and edit";
                }
                if (this.statusCode == HttpStatusCode.InternalServerError)
                {
                    TextDetailResult = "We detected an error.\n Please go back and edit";
                }
            }
        }

        private string imageResult;
        public string ImageResult
        {
            get => imageResult;
            set
            {
                SetAndRaisePropertyChanged(ref imageResult, value);
            }
        }

        private string textResult;
        public string TextResult
        {
            get => textResult;
            set
            {
                SetAndRaisePropertyChanged(ref textResult, value);
            }
        }


        public string TextDetailResult
        {
            get => textDetailResult;
            set
            {
                SetAndRaisePropertyChanged(ref textDetailResult, value);
            }
        }

        public ICommand EditYourSubmissionCommand => new Command(async () => await EditYourSubmissionCommandExecute());

        private async Task EditYourSubmissionCommandExecute()
        {
            await NavigateBackAsync();
        }

      
    }
}
