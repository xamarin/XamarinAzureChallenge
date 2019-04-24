using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinAzureChallenge.ViewModels
{
    public class ResultViewModel : BaseViewModel
    {
        private string textDetailResult;
        private string imageResult;
        private string textResult;

        public ResultViewModel(HttpStatusCode status)
        {
            EditYourSubmissionCommand = new Command(async () => await EditYourSubmissionCommandExecute());

            switch (status)
            {
                case HttpStatusCode.OK:
                    ImageResult = "resultOk";
                    TextResult = "Congratulations!";
                    TextDetailResult = "You have successfully submited the \n Challenge Form!";
                    break;

                case HttpStatusCode.BadRequest:
                    ImageResult = "resultFailed";
                    TextResult = "Oops!";
                    TextDetailResult = "We detected duplicated data.\n Please go back and edit";
                    break;

                case HttpStatusCode.InternalServerError:
                    ImageResult = "resultFailed";
                    TextResult = "Oops!";
                    TextDetailResult = "There was an error on the Azure Function.\n Please go back and edit";
                    break;

                default:
                    ImageResult = "resultFailed";
                    TextResult = "Oops!";
                    TextDetailResult = "Could not connect to the Azure Function.";
                    break;
            }
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


        public string TextDetailResult
        {
            get => textDetailResult;
            set => SetAndRaisePropertyChanged(ref textDetailResult, value);
        }

        private Task EditYourSubmissionCommandExecute() => NavigateBackAsync();
    }
}
