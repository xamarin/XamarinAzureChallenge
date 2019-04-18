using Xamarin.Forms;
using XamarinAzureChallenge.ViewModels;

namespace XamarinAzureChallenge.Pages
{
    public abstract class BaseContentPage<T> : ContentPage
      where T : BaseViewModel
    {
        protected virtual T ViewModel => BindingContext as T;
    }
}
