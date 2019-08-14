using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinAzureChallenge.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetAndRaisePropertyChanged<TRef>(ref TRef field, TRef value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<TRef>.Default.Equals(field, value))
                return;

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected Task NavigateToPage(Page page) => Device.InvokeOnMainThreadAsync(() => Application.Current.MainPage.Navigation.PushAsync(page));

        protected Task NavigateBack() => Device.InvokeOnMainThreadAsync(() => Application.Current.MainPage.Navigation.PopAsync());

        protected Task ShowFeatureNotAvailable() => Device.InvokeOnMainThreadAsync(() => Application.Current.MainPage.DisplayAlert("Feature not available", "", "Ok"));

        protected Task DisplayInvalidFieldAlert(string message) => Device.InvokeOnMainThreadAsync(() => Application.Current.MainPage.DisplayAlert("Invalid Field", message, "Ok"));
    }
}
